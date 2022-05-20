using IronMacbeth.BFF.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Contract = IronMacbeth.BFF.Contract;
using Image = IronMacbeth.Client.Model.Image;
using Internal = IronMacbeth.Client;

namespace IronMacbeth.Client
{
    class ServerAdapter
    {
        private static ServerAdapter _instance;
        public static ServerAdapter Instance => _instance ?? throw new Exception($"{nameof(ServerAdapter)} was not initialized. Use '{nameof(Initialize)}' method.");

        public static void Initialize(IService proxy)
        {
            if (_instance != null)
            {
                throw new Exception($"{nameof(ServerAdapter.Instance)} was already initialized.");
            }

            _instance = new ServerAdapter(proxy);
        }

        public static void ClearInstance()
        {
            _instance = null;
        }

        private IService _proxy;

        private ServerAdapter(IService proxy)
        {
            _proxy = proxy;
        }

        #region Order

        public void CreateOrder(Internal.Order orderInfo)
        {

            var contractOrder = MapInternalToContractCreateOrder(orderInfo);
            _proxy.CreateOrder(contractOrder);
        }

        private Contract.CreateOrder MapInternalToContractCreateOrder(Internal.Order orderInfo)
        {
            return new Contract.CreateOrder
            {
                Id = orderInfo.Id,
                UserLogin = orderInfo.UserLogin,
                BookId = orderInfo.Book?.Id,
                ArticleId = orderInfo.Article?.Id,
                PeriodicalId = orderInfo.Periodical?.Id,
                NewspaperId = orderInfo.Newspaper?.Id,
                ThesesId = orderInfo.Thesis?.Id,
                TypeOfOrder = orderInfo.TypeOfOrder,
                StatusOfOrder = orderInfo.StatusOfOrder,
                ReceiveDate = orderInfo.ReceiveDate,
                DateOfOrer = orderInfo.DateOfOrder,
                DateOfReturn = orderInfo.DateOfReturn,
                UserName = orderInfo.UserName,
                UserSurname = orderInfo.UserSurname,
                PhoneNumber = orderInfo.PhoneNumber

            };
        }

        public List<Internal.Order> GetAllOrders()
        {
            var orders = _proxy.GetAllOrders();

            var internalOrders = orders.Select(MapContractToInternalOrder).ToList();

            return internalOrders;
        }

        public bool CheckOrder(int id, DocumentType documentType)
        {
            var isAlreadyTheSameOrderInDb = _proxy.CheckOrder(id, documentType);
            return isAlreadyTheSameOrderInDb;
        }

        private Internal.Order MapContractToInternalOrder(Contract.Order order)
        {
            return new Internal.Order
            {
                Id = order.Id,
                UserLogin = order.UserLogin,
                TypeOfOrder = order.TypeOfOrder,
                StatusOfOrder = order.StatusOfOrder,
                DateOfOrder = order.DateOfOrder,
                DateOfReturn = order.DateOfReturn,
                ReceiveDate = order.ReceiveDate,
                UserSurname = order.UserSurname,
                UserName = order.UserName,
                PhoneNumber = order.PhoneNumber,
                Book = order.Book != null ? MapContractToInternalBook(order.Book) : null,
                Article = order.Article != null ? MapContractToInternalArticle(order.Article) : null,
                Periodical = order.Periodical != null ? MapContractToInternalPeriodical(order.Periodical) : null,
                Newspaper = order.Newspaper != null ? MapContractToInternalNewspaper(order.Newspaper) : null,
                Thesis = order.Theses != null ? MapContractToInternalTheses(order.Theses) : null
            };
        }

        public void DeleteOrder(int id)
        {
            _proxy.DeleteOrder(id);
        }

        public void UpdateOrder(Internal.Order order, Internal.SpecifiedOrderFields specifyOrderFields)
        {
            var contractOrder = MapInternalToContractUpdateOrder(order);
            var contractSpecifiedOrderFields = MapInternalToContractOrderFields(specifyOrderFields);
            _proxy.UpdateOrder(contractOrder, contractSpecifiedOrderFields);
        }

        private Contract.Order MapInternalToContractUpdateOrder(Internal.Order order)
        {
            return new Contract.Order
            {
                Id = order.Id,
                UserLogin = order.UserLogin,
                BookId = order.Book?.Id,
                ArticleId = order.Article?.Id,
                PeriodicalId = order.Periodical?.Id,
                NewspaperId = order.Newspaper?.Id,
                ThesesId = order.Thesis?.Id,
                TypeOfOrder = order.TypeOfOrder,
                DateOfOrder = order.DateOfOrder,
                UserName = order.UserName,
                UserSurname = order.UserSurname,
                PhoneNumber = order.PhoneNumber

            };
        }
        private Contract.SpecifiedOrderFields MapInternalToContractOrderFields(Internal.SpecifiedOrderFields specifyOrderFields)
        {
            return new Contract.SpecifiedOrderFields
            {
                DateOfReturning = specifyOrderFields.DateOfReturning,
                Status = specifyOrderFields.Status,
                ReceiveDate = specifyOrderFields.ReceiveDate
            };
        }

        #endregion

        #region SearchDocument

        public DocumentsSearchResults SearchDocuments(Internal.SearchFilledFields searchFilledFields)
        {
            var contractSearch = MapInternalToContractSearchCriteria(searchFilledFields);
            var documents = _proxy.SearchDocuments(contractSearch);
            var internalBooks = MapContractToInternalSearchCriteria(documents);
            return internalBooks;
        }

        private Contract.SearchFilledFields MapInternalToContractSearchCriteria(Internal.SearchFilledFields searchFilledFields)
        {
            return new Contract.SearchFilledFields
            {
                SearchName = searchFilledFields.SearchName,
                SearchAuthor = searchFilledFields.SearchAuthor,
                Topic = searchFilledFields.Topic,
                SearchYearFrom = searchFilledFields.SearchYearFrom,
                SearchYearTo = searchFilledFields.SearchYearTo,
                IsArticleSelected = searchFilledFields.IsArticleSelected,
                IsBookSelected = searchFilledFields.IsBookSelected,
                IsNewspaperSelected = searchFilledFields.IsNewspaperSelected,
                IsPeriodicalSelected = searchFilledFields.IsPeriodicalSelected,
                IsThesisSelected = searchFilledFields.IsThesisSelected

            };
        }

        private DocumentsSearchResults MapContractToInternalSearchCriteria(Contract.DocumentsSearchResults documentsSearchResults)
        {
            var documents = new DocumentsSearchResults();

            documents.Books = documentsSearchResults.Books?.Select(MapContractToInternalBook).ToList() ?? new List<Book>();

            foreach (var book in documents.Books)
            {
                PopulateIDisplayable(book);
                PopulateElectronicVersion(book);
            }

            documents.Articles = documentsSearchResults.Articles?.Select(MapContractToInternalArticle).ToList() ?? new List<Article>();

            foreach (var articles in documents.Articles)
            {
                PopulateElectronicVersion(articles);
            }

            documents.Periodicals = documentsSearchResults.Periodicals?.Select(MapContractToInternalPeriodical).ToList() ?? new List<Periodical>();

            foreach (var periodical in documents.Periodicals)
            {
                PopulateIDisplayable(periodical);
                PopulateElectronicVersion(periodical);
            }

            documents.Newspapers = documentsSearchResults.Newspapers?.Select(MapContractToInternalNewspaper).ToList() ?? new List<Newspaper>();

            foreach (var newspaper in documents.Newspapers)
            {
                PopulateElectronicVersion(newspaper);
            }

            documents.Theses = documentsSearchResults.Theses?.Select(MapContractToInternalTheses).ToList() ?? new List<Thesis>();

            foreach (var theses in documents.Theses)
            {
                PopulateElectronicVersion(theses);
            }

            return documents;
        }

        #endregion

        #region Periodical

        public void CreatePeriodical(Periodical periodical)
        {
            CreateIDisplayable(periodical);
            SaveElectronicVersion(periodical);

            var contractPeriodical = MapInternalToContractPeriodical(periodical);

            _proxy.CreatePeriodical(contractPeriodical);
        }

        public List<Internal.Periodical> GetAllPeriodicals()
        {
            var periodicals = _proxy.GetAllPeriodicals();

            var internalPeriodicals = periodicals.Select(MapContractToInternalPeriodical).ToList();

            foreach (var periodical in internalPeriodicals)
            {
                PopulateIDisplayable(periodical);
                PopulateElectronicVersion(periodical);
            }

            return internalPeriodicals;
        }

        public void UpdatePeriodical(Periodical periodical)
        {
            UpdateIDisplayable(periodical);
            UpdateElectronicVersion(periodical);

            var contractPeriodical = MapInternalToContractPeriodical(periodical);

            _proxy.UpdatePeriodical(contractPeriodical);
        }

        public void DeletePeriodical(int id)
        {
            _proxy.DeletePeriodical(id);
        }

        private Contract.Periodical MapInternalToContractPeriodical(Periodical periodical)
        {
            return new Contract.Periodical
            {
                Id = periodical.Id,
                Name = periodical.Name,
                Year = periodical.Year,
                Pages = periodical.Pages,
                Topic = periodical.Topic,
                City = periodical.City,
                Location = periodical.Location,
                PublishingHouse = periodical.PublishingHouse,
                Availiability = periodical.Availiability,
                TypeOfDocument = periodical.TypeOfDocument,
                IssueNumber = periodical.IssueNumber,
                Responsible = periodical.Responsible,
                RentPrice = periodical.RentPrice,
                ElectronicVersionFileId = periodical.ElectronicVersionFileId,
                ImageFileId = periodical.ImageFileId,
            };
        }

        private Internal.Periodical MapContractToInternalPeriodical(Contract.Periodical periodical)
        {
            return new Internal.Periodical
            {
                Id = periodical.Id,
                Name = periodical.Name,
                Year = periodical.Year,
                Pages = periodical.Pages,
                Availiability = periodical.Availiability,
                Topic = periodical.Topic,
                City = periodical.City,
                PublishingHouse = periodical.PublishingHouse,
                Location = periodical.Location,
                TypeOfDocument = periodical.TypeOfDocument,
                ElectronicVersionFileId = periodical.ElectronicVersionFileId,
                IssueNumber = periodical.IssueNumber,
                Responsible = periodical.Responsible,
                RentPrice = periodical.RentPrice,

                ImageFileId = periodical.ImageFileId
            };
        }

        #endregion

        #region Newspaper

        public void CreateNewspaper(Newspaper newspaper)
        {
            SaveElectronicVersion(newspaper);

            var contractNewspaper = MapInternalToContractNewspaper(newspaper);

            _proxy.CreateNewspaper(contractNewspaper);
        }

        public List<Internal.Newspaper> GetAllNewspapers()
        {
            var newspapers = _proxy.GetAllNewspapers();

            var internalNewspapers = newspapers.Select(MapContractToInternalNewspaper).ToList();

            foreach (var newspaper in internalNewspapers)
            {
                PopulateElectronicVersion(newspaper);
            }

            return internalNewspapers;
        }

        public void UpdateNewspaper(Internal.Newspaper newspaper)
        {
            UpdateElectronicVersion(newspaper);

            var contractNewspaper = MapInternalToContractNewspaper(newspaper);

            _proxy.UpdateNewspaper(contractNewspaper);
        }

        public void DeleteNewspaper(int id)
        {
            _proxy.DeleteNewspaper(id);
        }

        private Contract.Newspaper MapInternalToContractNewspaper(Internal.Newspaper newspaper)
        {
            return new Contract.Newspaper
            {
                Id = newspaper.Id,
                Name = newspaper.Name,
                Year = newspaper.Year,
                Topic = newspaper.Topic,
                City = newspaper.City,
                Availiability = newspaper.Availiability,
                TypeOfDocument = newspaper.TypeOfDocument,
                IssueNumber = newspaper.IssueNumber,
                RentPrice = newspaper.RentPrice,
                Location = newspaper.Location,
                ElectronicVersionFileId = newspaper.ElectronicVersionFileId,


            };
        }

        private Internal.Newspaper MapContractToInternalNewspaper(Contract.Newspaper newspaper)
        {
            return new Internal.Newspaper
            {
                Id = newspaper.Id,
                Name = newspaper.Name,
                Year = newspaper.Year,
                City = newspaper.City,
                Topic = newspaper.Topic,
                Availiability = newspaper.Availiability,
                TypeOfDocument = newspaper.TypeOfDocument,
                ElectronicVersionFileId = newspaper.ElectronicVersionFileId,
                IssueNumber = newspaper.IssueNumber,
                Location = newspaper.Location,
                RentPrice = newspaper.RentPrice,

            };
        }

        #endregion

        #region Thesis

        public void CreateThesis(Internal.Thesis thesis)
        {
            SaveElectronicVersion(thesis);

            var contractThesis = MapInternalToContractThesis(thesis);

            _proxy.CreateThesis(contractThesis);
        }

        public List<Internal.Thesis> GetAllThesises()
        {
            var thesises = _proxy.GetAllThesises();

            var internalThesises = thesises.Select(MapContractToInternalTheses).ToList();

            foreach (var thesis in internalThesises)
            {
                PopulateElectronicVersion(thesis);
            }

            return internalThesises;
        }

        public void UpdateThesis(Internal.Thesis thesis)
        {
            UpdateElectronicVersion(thesis);

            var contractThesis = MapInternalToContractThesis(thesis);

            _proxy.UpdateThesis(contractThesis);
        }

        public void DeleteThesis(int id)
        {
            _proxy.DeleteThesis(id);
        }

        private Contract.Thesis MapInternalToContractThesis(Internal.Thesis thesis)
        {
            return new Contract.Thesis
            {
                Id = thesis.Id,
                Name = thesis.Name,
                Year = thesis.Year,
                Topic = thesis.Topic,
                Author = thesis.Author,
                Pages = thesis.Pages,
                City = thesis.City,
                TypeOfDocument = thesis.TypeOfDocument,
                Responsible = thesis.Responsible,
                ElectronicVersionFileId = thesis.ElectronicVersionFileId,


            };
        }

        private Internal.Thesis MapContractToInternalTheses(Contract.Thesis thesis)
        {
            return new Internal.Thesis
            {
                Id = thesis.Id,
                Name = thesis.Name,
                Year = thesis.Year,
                Topic = thesis.Topic,
                Author = thesis.Author,
                City = thesis.City,
                Pages = thesis.Pages,
                TypeOfDocument = thesis.TypeOfDocument,
                Responsible = thesis.Responsible,
                ElectronicVersionFileId = thesis.ElectronicVersionFileId,


            };
        }

        #endregion

        #region Article

        public void CreateArticle(Internal.Article article)
        {
            SaveElectronicVersion(article);

            var contractArticle = MapInternalToContractArticle(article);

            _proxy.CreateArticle(contractArticle);
        }

        public List<Internal.Article> GetAllArticles()
        {
            var articles = _proxy.GetAllArticles();

            var internalArticles = articles.Select(MapContractToInternalArticle).ToList();

            foreach (var article in internalArticles)
            {
                PopulateElectronicVersion(article);
            }

            return internalArticles;
        }

        public void UpdateArticle(Article article)
        {
            UpdateElectronicVersion(article);

            var contractArticle = MapInternalToContractArticle(article);

            _proxy.UpdateArticle(contractArticle);
        }

        public void DeleteArticle(int id)
        {
            _proxy.DeleteArticle(id);
        }

        private Contract.Article MapInternalToContractArticle(Internal.Article article)
        {
            return new Contract.Article
            {
                Id = article.Id,
                Name = article.Name,
                Author = article.Author,
                Topic = article.Topic,
                Year = article.Year,
                Pages = article.Pages,
                MainDocumentId = article.MainDocumentId,
                TypeOfDocument = article.TypeOfDocument,
                ElectronicVersionFileId = article.ElectronicVersionFileId,

            };
        }

        private Internal.Article MapContractToInternalArticle(Contract.Article article)
        {
            return new Internal.Article
            {
                Id = article.Id,
                Name = article.Name,
                Author = article.Author,
                Topic = article.Topic,
                Year = article.Year,
                Pages = article.Pages,
                MainDocumentId = article.MainDocumentId,
                TypeOfDocument = article.TypeOfDocument,
                ElectronicVersionFileId = article.ElectronicVersionFileId,

            };
        }

        #endregion

        #region Book

        public void CreateBook(Internal.Book book)
        {
            CreateIDisplayable(book);
            SaveElectronicVersion(book);

            var contractBook = MapInternalToContractBook(book);

            _proxy.CreateBook(contractBook);
        }

        public List<Book> GetAllBooks()
        {
            var books = _proxy.GetAllBooks();

            var internalBooks = books.Select(MapContractToInternalBook).ToList();

            foreach (var book in internalBooks)
            {
                PopulateIDisplayable(book);
                PopulateElectronicVersion(book);
            }

            return internalBooks;
        }

        public void UpdateBook(Internal.Book book)
        {
            UpdateIDisplayable(book);
            UpdateElectronicVersion(book);

            var contractBook = MapInternalToContractBook(book);

            _proxy.UpdateBook(contractBook);
        }

        public void DeleteBook(int id)
        {
            _proxy.DeleteBook(id);
        }

        private Contract.Book MapInternalToContractBook(Internal.Book book)
        {
            return new Contract.Book
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Topic = book.Topic,
                PublishingHouse = book.PublishingHouse,
                City = book.City,
                Year = book.Year,
                Pages = book.Pages,
                Availiability = book.Availiability,
                Location = book.Location,
                TypeOfDocument = book.TypeOfDocument,
                RentPrice = book.RentPrice,
                ElectronicVersionFileId = book.ElectronicVersionFileId,

                ImageFileId = book.ImageFileId
            };
        }

        private Internal.Book MapContractToInternalBook(Contract.Book book)
        {
            return new Internal.Book
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Topic = book.Topic,
                PublishingHouse = book.PublishingHouse,
                City = book.City,
                Year = book.Year,
                Pages = book.Pages,
                Availiability = book.Availiability,
                Location = book.Location,
                TypeOfDocument = book.TypeOfDocument,
                RentPrice = book.RentPrice,
                ElectronicVersionFileId = book.ElectronicVersionFileId,

                ImageFileId = book.ImageFileId
            };
        }

        #endregion

        #region User

        public Internal.User LogIn()
        {
            var result = _proxy.GetLoggedInUser();

            if (result != null)
            {
                return MapContractToInternalUser(result);
            }

            return null;
        }

        private User MapContractToInternalUser(Contract.User user)
        {
            return new User
            {
                Login = user.Login,
                UserRole = user.UserRole,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber
            };
        }

        #endregion

        private void PopulateIDisplayable(IDisplayable displayable)
        {
            if (displayable.ImageFileId != null)
            {
                var imageStream = new MemoryStream();

                using (Stream serverStream = _proxy.GetFile(displayable.ImageFileId.Value))
                {
                    serverStream.CopyTo(imageStream);
                }

                imageStream.Seek(0, SeekOrigin.Begin);

                displayable.Image = new Image(imageStream);
            }
        }

        private void PopulateElectronicVersion(Document document)
        {
            if (document.ElectronicVersionFileId != null)
            {
                var documentElectronicVersion = new MemoryStream();

                using (Stream serverStream = _proxy.GetFile(document.ElectronicVersionFileId.Value))
                {
                    serverStream.CopyTo(documentElectronicVersion);
                }

                documentElectronicVersion.Seek(0, SeekOrigin.Begin);

                document.ElectronicVersion = documentElectronicVersion;
            }
        }

        private void CreateIDisplayable(IDisplayable displayable)
        {
            if (displayable.Image != null)
            {
                displayable.ImageFileId = _proxy.AddFile(displayable.Image.ImageData);
            }
        }

        private void SaveElectronicVersion(Document document)
        {
            if (document.ElectronicVersion != null)
            {
                document.ElectronicVersionFileId = _proxy.AddFile(document.ElectronicVersion);
                document.ElectronicVersion.Seek(0, SeekOrigin.Begin);
            }
        }

        private void UpdateIDisplayable(IDisplayable displayable)
        {
            if (displayable.ImageFileId == null && displayable.Image != null)
            {
                displayable.ImageFileId = _proxy.AddFile(displayable.Image.ImageData);
            }
        }

        private void UpdateElectronicVersion(Document document)
        {
            if (document.ElectronicVersionFileId == null && document.ElectronicVersion != null)
            {
                document.ElectronicVersionFileId = _proxy.AddFile(document.ElectronicVersion);
            }
        }
    }
}