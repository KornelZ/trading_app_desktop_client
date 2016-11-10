using Microsoft.VisualStudio.TestTools.UnitTesting;
using LGSA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGSA.Model.Services;
using Moq;
using LGSA.Model;
using System.Linq.Expressions;
using LGSA.Utility;
using LGSA.Model.ModelWrappers;

namespace LGSA.ViewModel.Tests
{
    [TestClass()]
    public class ProductViewModelTests
    {

        [TestMethod()]
        public async void LoadShouldUseProperFilter()
        {
            //before
            Expression<Func<product, bool>> filter = null;
            Expression<Func<product, bool>> modelFilter =  p => p.Name.Contains("asd")
                && p.users.First_Name == null && p.users.Last_Name == null &&
                p.rating >= null && p.dic_condition.name.Contains(null) &&
                p.dic_Genre.name.Contains(null) && p.stock >= null;

            FilterViewModel FilterView = new FilterViewModel();
            FilterView.Name = "asd";
            Mock<ProductService> service = new Mock<ProductService>();
            service.Setup(x => x.GetData(It.IsAny<Expression<Func<product, bool>>>()))
                .Returns(It.IsAny<Task<IEnumerable<product>>>())
                .Callback<Expression<Func<product, bool>>>( r => filter = r);
            ProductViewModel view = new ProductViewModel(null, null, null, null);

            //call
            await view.Load();

            //after
            Assert.AreEqual(modelFilter, filter);
        }

        [TestMethod()]
        public async void LoadShouldSetProperProducts()
        {
            //before
            Mock<ProductService> service = new Mock<ProductService>();
            List<product> collection = new List<product>();
            collection.Add(new product());
            var responseTask = Task.FromResult(collection as IEnumerable<product>);
            service.Setup(x => x.GetData(It.IsAny<Expression<Func<product, bool>>>())).Returns(responseTask);
            ProductViewModel view = new ProductViewModel(null, null, null, null);

            //call
            await view.Load();

            //after
            Assert.AreEqual(view.Products.Count, collection.Count);
        }

        
        [TestMethod()]
        public void UpdateTestShouldNotBeExecutedOnNullArgument()
        {
            //before
            Mock<ProductService> service = new Mock<ProductService>();
            service.Setup(x => x.Update(It.IsAny<product>())).Returns(It.IsAny<Task<bool>>());
            ProductViewModel view = new ProductViewModel(null, null, null, null);
            view.SelectedProduct = null;

            //call
            view.Update();

            //after
            service.Verify(e => e.Update(It.IsAny<product>()), Times.Never);
        }
        [TestMethod()]
        public async void UpdateTestShouldInvokeUpdateOnServiceAtLeastOnce()
        {
            //before
            Mock<ProductService> service = new Mock<ProductService>();
            service.Setup(x => x.Update(It.IsAny<product>())).Returns(It.IsAny<Task<bool>>());
            ProductViewModel view = new ProductViewModel(null, null, null, null);
            view.SelectedProduct = new Model.ModelWrappers.ProductWrapper(new product());

            //call
            await view.Update();

            //after
            service.Verify(e => e.Update(It.IsAny<product>()), Times.AtLeastOnce);
        }
    }
}