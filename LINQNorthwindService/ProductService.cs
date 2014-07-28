using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MyWCFServices.LINQNorthwindEntities;
using MyWCFServices.LINQNorthwindLogic;

namespace MyWCFServices.LINQNorthwindService
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in App.config.
    public class ProductService : IProductService
    {
        ProductLogic productLogic = new ProductLogic();
        public Product GetProduct(int id)
        {
            /*
            // TODO: call business logic layer to retrieve product
            Product product = new Product();
            product.ProductID = id;
            product.ProductName =
            "fake product name from service layer";
            product.UnitPrice = (decimal)10.0;
            */
            ProductEntity productEntity = productLogic.GetProduct(id);
            Product product = new Product();
            if (productEntity == null)
            {
                //    throw new Exception("No product found with id " + id);
                if (id != 999)
                    throw new FaultException<ProductFault>(new ProductFault("No product found with id " + id), "Product Fault");
                else
                    throw new Exception("Test Exception");
            }
            TranslateProductEntityToProductContractData(productEntity, product);
            return product;
        }
        public bool UpdateProduct(Product product)
        {
            /*
            // TODO: call business logic layer to update product
            if (product.UnitPrice <= 0)
            return false;
            else
            return true;
            */
            ProductEntity productEntity = new ProductEntity();
            TranslateProductContractDataToProductEntity(
            product, productEntity);
            return productLogic.UpdateProduct(productEntity);
        }
        private void TranslateProductEntityToProductContractData(
        ProductEntity productEntity,
        Product product)
        {
            product.ProductID = productEntity.ProductID;
            product.ProductName = productEntity.ProductName;
            product.QuantityPerUnit = productEntity.QuantityPerUnit;
            product.UnitPrice = productEntity.UnitPrice;
            product.Discontinued = productEntity.Discontinued;
        }
        private void TranslateProductContractDataToProductEntity(
        Product product,
        ProductEntity productEntity)
        {
            productEntity.ProductID = product.ProductID;
            productEntity.ProductName = product.ProductName;
            productEntity.QuantityPerUnit = product.QuantityPerUnit;
            productEntity.UnitPrice = product.UnitPrice;
            productEntity.Discontinued = product.Discontinued;
        }
    }
}