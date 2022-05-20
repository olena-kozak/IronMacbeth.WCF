using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace IronMacbeth.BFF.Contract
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        DocumentsSearchResults SearchDocuments(SearchFilledFields searchFilledFields);

        #region Order
        [OperationContract]
        void CreateOrder(CreateOrder orderInfo);

        [OperationContract]
        void UpdateOrder(Order order, SpecifiedOrderFields specifyOrderFieldso);

        [OperationContract]
        List<Order> GetAllOrders();

        [OperationContract]
        bool CheckOrder(int id, DocumentType documentType);


        [OperationContract]
        void DeleteOrder(int id);

        #endregion


        #region Book

        [OperationContract]
        void CreateBook(Book book);



        [OperationContract]
        List<Book> GetAllBooks();

        [OperationContract]
        void UpdateBook(Book book);

        [OperationContract]
        void DeleteBook(int id);

        #endregion

        #region Article
        [OperationContract]
        void CreateArticle(Article article);

        [OperationContract]
        List<Article> GetAllArticles();

        [OperationContract]
        void UpdateArticle(Article article);

        [OperationContract]
        void DeleteArticle(int id);
        #endregion

        #region Periodical

        [OperationContract]
        void CreatePeriodical(Periodical periodical);

        [OperationContract]
        List<Periodical> GetAllPeriodicals();

        [OperationContract]
        void UpdatePeriodical(Periodical periodical);

        [OperationContract]
        void DeletePeriodical(int id);
        #endregion

        #region Thesis

        [OperationContract]
        void CreateThesis(Thesis thesis);

        [OperationContract]
        List<Thesis> GetAllThesises();

        [OperationContract]
        void UpdateThesis(Thesis thesis);

        [OperationContract]
        void DeleteThesis(int id);
        #endregion

        #region Newspaper

        [OperationContract]
        void CreateNewspaper(Newspaper newspaper);

        [OperationContract]
        List<Newspaper> GetAllNewspapers();

        [OperationContract]
        void UpdateNewspaper(Newspaper newspaper);

        [OperationContract]
        void DeleteNewspaper(int id);
        #endregion

        #region User

        [OperationContract]
        User GetLoggedInUser();

        #endregion

        [OperationContract]
        Guid AddFile(Stream fileStream);

        [OperationContract]
        Stream GetFile(Guid fileId);
    }
}