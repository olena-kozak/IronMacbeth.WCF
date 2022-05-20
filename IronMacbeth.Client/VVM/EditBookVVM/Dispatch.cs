using IronMacbeth.Client.ViewModel;
using IronMacbeth.Client.VVM.BookVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronMacbeth.Client.VVM.EditBookVVM
{
    class Dispatch
    {

        IHandler[] Handlers;

        public Dispatch(IHandler[] handlers) => Handlers = handlers;

        public FilledFieldsInfo UnwrapObjectForEdit(object objectForUnwrapping)
        {
            var handler = Handlers.SingleOrDefault(x => x.CanHandleUnwrapping(objectForUnwrapping));
            if (handler != null)
            {
                return handler.Unwrap(objectForUnwrapping);
            }
            else
            {
                throw new InvalidOperationException("Can not handle operation");
            }

        }
        public void DeleteDispatch(object objectForUnwrapping)
        {
            var handler = Handlers.SingleOrDefault(x => x.CanHandleUnwrapping(objectForUnwrapping));
            if (handler != null)
            {
                handler.HandleDelete(objectForUnwrapping);
            }
            else
            {
                throw new InvalidOperationException("Can not handle operation");
            }
        }

        public void DispatchCreation(FilledFieldsInfo filledFieldsInfo)
        {

            var handler = Handlers.SingleOrDefault(x => x.CandHandle(filledFieldsInfo));
            if (handler != null)
            {
                handler.HandlerCreation(filledFieldsInfo);
            }
            else
            {
                throw new InvalidOperationException("Can not handle operation");
            }

        }

        public bool CanExecuteApplyChanges(FilledFieldsInfo filledFieldsInfo)
        {
            var handler = Handlers.SingleOrDefault(x => x.CandHandle(filledFieldsInfo));
            if (handler != null)
            {
                return handler.CandExecuteApplyChanges(filledFieldsInfo);
            }
            else
            {
                return false;
            }
        }

        public void DispatchUpdate(FilledFieldsInfo filledFieldsInfo, object objectForEdit)
        {
            var handler = Handlers.SingleOrDefault(x => x.CandHandle(filledFieldsInfo));
            if (handler != null)
            {
                handler.HandleUpdate(filledFieldsInfo, objectForEdit);
            }
            else
            {
                throw new InvalidOperationException("Can not handle operation");
            }
        }
    }

    interface IHandler
    {
        void HandlerCreation(FilledFieldsInfo filledFieldsInfo);
        void HandleUpdate(FilledFieldsInfo filledFieldsInfo, object objectForEdit);
        void HandleDelete(object objectForEdit);
        bool CandHandle(FilledFieldsInfo filledFieldsInfo);
        bool CandHandleUpdate(FilledFieldsInfo filledFieldsInfo);
        bool CandExecuteApplyChanges(FilledFieldsInfo filledFieldsInfo);
        bool CanHandleUnwrapping(object objectForUnwrapping);
        FilledFieldsInfo Unwrap(object objectForUnwrapping);

    }

    #region BookHandler
    public class BookHandler : IHandler
    {
        public bool CandHandle(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Book")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool CandExecuteApplyChanges(FilledFieldsInfo filledFieldsInfo)
        {
            return !string.IsNullOrWhiteSpace(filledFieldsInfo.Name) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Author) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.PublishingHouse) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.RentPrice) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Topic) &&
                filledFieldsInfo.Pages != null &&
                filledFieldsInfo.Availiability != null &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Location) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.TypeOfDocument);
        }

        public bool CandHandleUpdate(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Book")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanHandleUnwrapping(object objectForUnwrapping)
        {
            if (objectForUnwrapping is Book)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HandleDelete(object objectToDelete)
        {
            Book book = objectToDelete as Book;
            ServerAdapter.Instance.DeleteBook(book.Id);
        }

        public void HandlerCreation(FilledFieldsInfo filledFieldsInfo)
        {
            Book book = new Book
            {
                Image = filledFieldsInfo.Image,
                Name = filledFieldsInfo.Name,
                Author = filledFieldsInfo.Author,
                PublishingHouse = filledFieldsInfo.PublishingHouse,
                City = filledFieldsInfo.City,
                Year = filledFieldsInfo.Year.Value,
                Topic = filledFieldsInfo.Topic,
                Pages = filledFieldsInfo.Pages.Value,
                Availiability = filledFieldsInfo.Availiability.Value,
                Location = filledFieldsInfo.Location,
                TypeOfDocument = filledFieldsInfo.TypeOfDocument,
                RentPrice = filledFieldsInfo.RentPrice,
                ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId,
                ElectronicVersion = filledFieldsInfo.ElectronicVersion
            };

            ServerAdapter.Instance.CreateBook(book);
        }

        public void HandleUpdate(FilledFieldsInfo filledFieldsInfo, object objectForEdit)
        {
            Book book = objectForEdit as Book;
            book.Name = filledFieldsInfo.Name;
            book.Author = filledFieldsInfo.Author;
            book.PublishingHouse = filledFieldsInfo.PublishingHouse;                      //Handle exception
            book.City = filledFieldsInfo.City;
            book.Year = filledFieldsInfo.Year.Value;
            book.Pages = filledFieldsInfo.Pages.Value;
            book.Availiability = filledFieldsInfo.Availiability.Value;
            book.Location = filledFieldsInfo.Location;
            book.Topic = filledFieldsInfo.Topic;
            book.TypeOfDocument = filledFieldsInfo.TypeOfDocument;
            book.ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId;
            book.ElectronicVersion = filledFieldsInfo.ElectronicVersion;

            book.Image = filledFieldsInfo.Image;
            book.ImageFileId = filledFieldsInfo.ImageFileId;

            ServerAdapter.Instance.UpdateBook(book);
        }

        public FilledFieldsInfo Unwrap(object objectForUnwrapping)
        {
            if (!CanHandleUnwrapping(objectForUnwrapping)) { throw new InvalidOperationException(); }

            FilledFieldsInfo filledFieldsInfo = new FilledFieldsInfo();

            Book book = (Book)objectForUnwrapping;

            filledFieldsInfo.Image = book.Image;
            filledFieldsInfo.ImageFileId = book.ImageFileId;
            filledFieldsInfo.Name = book.Name;
            filledFieldsInfo.Author = book.Author;
            filledFieldsInfo.PublishingHouse = book.PublishingHouse;                      //TODO:Handle exception
            filledFieldsInfo.City = book.City;
            filledFieldsInfo.Year = book.Year;
            filledFieldsInfo.Topic = book.Topic;
            filledFieldsInfo.Pages = book.Pages;
            filledFieldsInfo.RentPrice = book.RentPrice;
            filledFieldsInfo.Availiability = book.Availiability;
            filledFieldsInfo.Location = book.Location;
            filledFieldsInfo.TypeOfDocument = book.TypeOfDocument;
            filledFieldsInfo.ElectronicVersionFileId = book.ElectronicVersionFileId;
            filledFieldsInfo.ElectronicVersion = book.ElectronicVersion;


            return filledFieldsInfo;
        }
    }

    #endregion

    #region ArticleHandler
    public class ArticleHandler : IHandler
    {
        public bool CandExecuteApplyChanges(FilledFieldsInfo filledFieldsInfo)
        {
            return !string.IsNullOrWhiteSpace(filledFieldsInfo.Name) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Author) &&
                 !string.IsNullOrWhiteSpace(filledFieldsInfo.Topic) &&
                filledFieldsInfo.Pages != null &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.TypeOfDocument) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.MainDocumentId); ;
        }

        public bool CandHandle(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Article")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CandHandleUpdate(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Article")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanHandleUnwrapping(object objectForUnwrapping)
        {
            if (objectForUnwrapping is Article)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HandleDelete(object objectForEdit)
        {
            Article article = objectForEdit as Article;
            ServerAdapter.Instance.DeleteArticle(article.Id);
        }

        public void HandlerCreation(FilledFieldsInfo filledFieldsInfo)
        {
            Article article = new Article
            {

                Name = filledFieldsInfo.Name,
                Author = filledFieldsInfo.Author,
                Year = filledFieldsInfo.Year.Value,
                Topic = filledFieldsInfo.Topic,
                Pages = filledFieldsInfo.Pages.Value,
                MainDocumentId = filledFieldsInfo.MainDocumentId,
                TypeOfDocument = filledFieldsInfo.TypeOfDocument,
                ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId,
                ElectronicVersion = filledFieldsInfo.ElectronicVersion,
            };

            ServerAdapter.Instance.CreateArticle(article);
        }

        public void HandleUpdate(FilledFieldsInfo filledFieldsInfo, object objectForEdit)
        {
            Article article = objectForEdit as Article;

            article.Name = filledFieldsInfo.Name;
            article.Author = filledFieldsInfo.Author;
            article.Topic = filledFieldsInfo.Topic;
            article.Year = filledFieldsInfo.Year.Value;
            article.Pages = filledFieldsInfo.Pages.Value;
            article.MainDocumentId = filledFieldsInfo.MainDocumentId;
            article.TypeOfDocument = filledFieldsInfo.TypeOfDocument;
            article.ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId;
            article.ElectronicVersion = filledFieldsInfo.ElectronicVersion;

            ServerAdapter.Instance.UpdateArticle(article);
        }


        public FilledFieldsInfo Unwrap(object objectForUnwrapping)
        {
            if (!CanHandleUnwrapping(objectForUnwrapping)) { throw new InvalidOperationException(); }
            FilledFieldsInfo filledFieldsInfo = new FilledFieldsInfo();
            Article article = (Article)objectForUnwrapping;

            filledFieldsInfo.Name = article.Name;
            filledFieldsInfo.Author = article.Author;
            filledFieldsInfo.Year = article.Year;
            filledFieldsInfo.Pages = article.Pages;
            filledFieldsInfo.Topic = article.Topic;
            filledFieldsInfo.MainDocumentId = article.MainDocumentId;
            filledFieldsInfo.TypeOfDocument = article.TypeOfDocument;
            filledFieldsInfo.ElectronicVersionFileId = article.ElectronicVersionFileId;
            filledFieldsInfo.ElectronicVersion = article.ElectronicVersion;

            return filledFieldsInfo;
        }
    }

    #endregion

    #region PeriodicalHandler
    public class PeriodicalHandler : IHandler
    {
        public bool CandExecuteApplyChanges(FilledFieldsInfo filledFieldsInfo)
        {
            return !string.IsNullOrWhiteSpace(filledFieldsInfo.Name) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Responsible) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.PublishingHouse) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Location) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Topic) &&
                 !string.IsNullOrWhiteSpace(filledFieldsInfo.RentPrice) &&
                  filledFieldsInfo.Availiability != null &&
                filledFieldsInfo.IssueNumber != null &&
                filledFieldsInfo.Pages != null &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.TypeOfDocument);
        }

        public bool CandHandle(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Periodical")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CandHandleUpdate(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Periodical")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanHandleUnwrapping(object objectForUnwrapping)
        {
            if (objectForUnwrapping is Periodical)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HandleDelete(object objectForEdit)
        {
            Periodical article = objectForEdit as Periodical;
            ServerAdapter.Instance.DeletePeriodical(article.Id);
        }

        public void HandlerCreation(FilledFieldsInfo filledFieldsInfo)
        {
            Periodical periodical = new Periodical
            {
                Name = filledFieldsInfo.Name,
                Year = filledFieldsInfo.Year.Value,
                Pages = filledFieldsInfo.Pages.Value,
                City = filledFieldsInfo.City,
                Topic = filledFieldsInfo.Topic,
                PublishingHouse = filledFieldsInfo.PublishingHouse,
                Availiability = filledFieldsInfo.Availiability.Value,
                TypeOfDocument = filledFieldsInfo.TypeOfDocument,
                Location = filledFieldsInfo.Location,
                IssueNumber = filledFieldsInfo.IssueNumber.Value,
                Responsible = filledFieldsInfo.Responsible,
                RentPrice = filledFieldsInfo.RentPrice,
                ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId,
                ElectronicVersion = filledFieldsInfo.ElectronicVersion,
                ImageFileId = filledFieldsInfo.ImageFileId,
                Image = filledFieldsInfo.Image
            };

            ServerAdapter.Instance.CreatePeriodical(periodical);
        }

        public void HandleUpdate(FilledFieldsInfo filledFieldsInfo, object objectForEdit)
        {
            Periodical periodical = objectForEdit as Periodical;
            periodical.Name = filledFieldsInfo.Name;
            periodical.Year = filledFieldsInfo.Year.Value;
            periodical.Pages = filledFieldsInfo.Pages.Value;
            periodical.City = filledFieldsInfo.City;
            periodical.Topic = filledFieldsInfo.Topic;
            periodical.PublishingHouse = filledFieldsInfo.PublishingHouse;
            periodical.Location = filledFieldsInfo.Location;
            periodical.Availiability = filledFieldsInfo.Availiability.Value;
            periodical.TypeOfDocument = filledFieldsInfo.TypeOfDocument;
            periodical.IssueNumber = filledFieldsInfo.IssueNumber.Value;
            periodical.Responsible = filledFieldsInfo.Responsible;
            periodical.RentPrice = filledFieldsInfo.RentPrice;
            periodical.ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId;
            periodical.ElectronicVersion = filledFieldsInfo.ElectronicVersion;

            periodical.Image = filledFieldsInfo.Image;
            periodical.ImageFileId = filledFieldsInfo.ImageFileId;

            ServerAdapter.Instance.UpdatePeriodical(periodical);
        }

        public FilledFieldsInfo Unwrap(object objectForUnwrapping)
        {
            if (!CanHandleUnwrapping(objectForUnwrapping)) { throw new InvalidOperationException(); }
            FilledFieldsInfo filledFieldsInfo = new FilledFieldsInfo();
            Periodical periodical = (Periodical)objectForUnwrapping;
            filledFieldsInfo.Image = periodical.Image;
            filledFieldsInfo.Name = periodical.Name;
            filledFieldsInfo.Year = periodical.Year;
            filledFieldsInfo.City = periodical.City;
            filledFieldsInfo.Topic = periodical.Topic;
            filledFieldsInfo.Pages = periodical.Pages;
            filledFieldsInfo.Location = periodical.Location;
            filledFieldsInfo.PublishingHouse = periodical.PublishingHouse;
            filledFieldsInfo.Availiability = periodical.Availiability;
            filledFieldsInfo.TypeOfDocument = periodical.TypeOfDocument;
            filledFieldsInfo.IssueNumber = periodical.IssueNumber;
            filledFieldsInfo.Responsible = periodical.Responsible;
            filledFieldsInfo.RentPrice = periodical.RentPrice;
            filledFieldsInfo.ElectronicVersionFileId = periodical.ElectronicVersionFileId;
            filledFieldsInfo.ElectronicVersion = periodical.ElectronicVersion;

            filledFieldsInfo.ImageFileId = periodical.ImageFileId;
            return filledFieldsInfo;
        }
    }

    #endregion

    #region ThesisHandler
    public class ThesisHandler : IHandler
    {
        public bool CandExecuteApplyChanges(FilledFieldsInfo filledFieldsInfo)
        {
            return !string.IsNullOrWhiteSpace(filledFieldsInfo.Name) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Topic) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Responsible) &&
                                 filledFieldsInfo.Pages != null &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.TypeOfDocument);
        }


        public bool CandHandle(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Thesis")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CandHandleUpdate(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Thesis")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanHandleUnwrapping(object objectForUnwrapping)
        {
            if (objectForUnwrapping is Thesis)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HandleDelete(object objectForEdit)
        {
            Thesis thesis = objectForEdit as Thesis;
            ServerAdapter.Instance.DeleteThesis(thesis.Id);
        }

        public void HandlerCreation(FilledFieldsInfo filledFieldsInfo)
        {
            Thesis thesis = new Thesis
            {
                Name = filledFieldsInfo.Name,
                Topic = filledFieldsInfo.Topic,
                Year = filledFieldsInfo.Year.Value,
                Author = filledFieldsInfo.Author,
                Pages = filledFieldsInfo.Pages.Value,
                City = filledFieldsInfo.City,
                TypeOfDocument = filledFieldsInfo.TypeOfDocument,
                Responsible = filledFieldsInfo.Responsible,
                ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId,
                ElectronicVersion = filledFieldsInfo.ElectronicVersion
            };

            ServerAdapter.Instance.CreateThesis(thesis);
        }

        public void HandleUpdate(FilledFieldsInfo filledFieldsInfo, object objectForEdit)
        {
            Thesis thesis = objectForEdit as Thesis;
            thesis.Name = filledFieldsInfo.Name;
            thesis.Year = filledFieldsInfo.Year.Value;
            thesis.Author = filledFieldsInfo.Author;
            thesis.Topic = filledFieldsInfo.Topic;
            thesis.Pages = filledFieldsInfo.Pages.Value;
            thesis.City = filledFieldsInfo.City;
            thesis.TypeOfDocument = filledFieldsInfo.TypeOfDocument;
            thesis.Responsible = filledFieldsInfo.Responsible;
            thesis.ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId;
            thesis.ElectronicVersion = filledFieldsInfo.ElectronicVersion;

            ServerAdapter.Instance.UpdateThesis(thesis);
        }

        public FilledFieldsInfo Unwrap(object objectForUnwrapping)
        {
            if (!CanHandleUnwrapping(objectForUnwrapping)) { throw new InvalidOperationException(); }
            FilledFieldsInfo filledFieldsInfo = new FilledFieldsInfo();
            Thesis thesis = (Thesis)objectForUnwrapping;

            filledFieldsInfo.Name = thesis.Name;
            filledFieldsInfo.Year = thesis.Year;
            filledFieldsInfo.Author = thesis.Author;
            filledFieldsInfo.City = thesis.City;
            filledFieldsInfo.Topic = thesis.Topic;
            filledFieldsInfo.Pages = thesis.Pages;
            filledFieldsInfo.TypeOfDocument = thesis.TypeOfDocument;
            filledFieldsInfo.Responsible = thesis.Responsible;
            filledFieldsInfo.ElectronicVersionFileId = thesis.ElectronicVersionFileId;
            filledFieldsInfo.ElectronicVersion = thesis.ElectronicVersion;

            return filledFieldsInfo;
        }
    }
    #endregion

    #region NewspaperHandler
    public class NewspaperHandler : IHandler
    {
        public bool CandExecuteApplyChanges(FilledFieldsInfo filledFieldsInfo)
        {
            return !string.IsNullOrWhiteSpace(filledFieldsInfo.Name) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Topic) &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.Location) &&
                 !string.IsNullOrWhiteSpace(filledFieldsInfo.RentPrice) &&
                  filledFieldsInfo.Availiability != null &&
                filledFieldsInfo.IssueNumber != null &&
                !string.IsNullOrWhiteSpace(filledFieldsInfo.TypeOfDocument);
        }

        public bool CandHandle(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Newspaper")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CandHandleUpdate(FilledFieldsInfo filledFieldsInfo)
        {
            if (filledFieldsInfo.TypeOfDocument == "Newspaper")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanHandleUnwrapping(object objectForUnwrapping)
        {
            if (objectForUnwrapping is Newspaper)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HandleDelete(object objectForEdit)
        {
            Newspaper newspaper = objectForEdit as Newspaper;
            ServerAdapter.Instance.DeleteNewspaper(newspaper.Id);
        }

        public void HandlerCreation(FilledFieldsInfo filledFieldsInfo)
        {
            Newspaper newspaper = new Newspaper
            {
                Name = filledFieldsInfo.Name,
                Year = filledFieldsInfo.Year.Value,
                IssueNumber = filledFieldsInfo.IssueNumber.Value,
                Topic = filledFieldsInfo.Topic,
                RentPrice = filledFieldsInfo.RentPrice,
                City = filledFieldsInfo.City,
                Availiability = filledFieldsInfo.Availiability.Value,
                TypeOfDocument = filledFieldsInfo.TypeOfDocument,
                Location = filledFieldsInfo.Location,
                ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId,
                ElectronicVersion = filledFieldsInfo.ElectronicVersion
            };

            ServerAdapter.Instance.CreateNewspaper(newspaper);
        }

        public void HandleUpdate(FilledFieldsInfo filledFieldsInfo, object objectForEdit)
        {
            Newspaper newspaper = objectForEdit as Newspaper;

            newspaper.Name = filledFieldsInfo.Name;
            newspaper.Year = filledFieldsInfo.Year.Value;
            newspaper.IssueNumber = filledFieldsInfo.IssueNumber.Value;
            newspaper.City = filledFieldsInfo.City;
            newspaper.Topic = filledFieldsInfo.Topic;
            newspaper.RentPrice = filledFieldsInfo.RentPrice;
            newspaper.Availiability = filledFieldsInfo.Availiability.Value;
            newspaper.TypeOfDocument = filledFieldsInfo.TypeOfDocument;
            newspaper.Location = filledFieldsInfo.Location;
            newspaper.ElectronicVersionFileId = filledFieldsInfo.ElectronicVersionFileId;
            newspaper.ElectronicVersion = filledFieldsInfo.ElectronicVersion;

            ServerAdapter.Instance.UpdateNewspaper(newspaper);
        }

        public FilledFieldsInfo Unwrap(object objectForUnwrapping)
        {
            if (!CanHandleUnwrapping(objectForUnwrapping)) { throw new InvalidOperationException(); }
            FilledFieldsInfo filledFieldsInfo = new FilledFieldsInfo();
            Newspaper newspaper = (Newspaper)objectForUnwrapping;

            filledFieldsInfo.Name = newspaper.Name;
            filledFieldsInfo.Year = newspaper.Year;
            filledFieldsInfo.City = newspaper.City;
            filledFieldsInfo.Topic = newspaper.Topic;
            filledFieldsInfo.Topic = newspaper.Topic;
            filledFieldsInfo.Availiability = newspaper.Availiability;
            filledFieldsInfo.IssueNumber = newspaper.IssueNumber;
            filledFieldsInfo.RentPrice = newspaper.RentPrice;
            filledFieldsInfo.TypeOfDocument = newspaper.TypeOfDocument;
            filledFieldsInfo.Location = newspaper.Location;
            filledFieldsInfo.ElectronicVersionFileId = newspaper.ElectronicVersionFileId;
            filledFieldsInfo.ElectronicVersion = newspaper.ElectronicVersion;

            return filledFieldsInfo;
        }
    }

    #endregion
}

