using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyWCFServices.LINQNorthwindEntities;
using MyWCFServices.LINQNorthwindDAL;

namespace MyWCFServices.LINQNorthwindLogic
{
    public class ProductLogic
    {
        ProductDAO productDAO = new ProductDAO();

        public ProductEntity GetProduct(int id)
        {
            // TODO: call data access layer to retrieve product
            ProductEntity p = new ProductEntity();
            p.ProductID = id;
            p.ProductName =
            "fake product name from business logic layer";
            p.UnitPrice = (decimal)20.0;
            if (id > 50) p.UnitsOnOrder = 30;
            return productDAO.GetProduct(id);
        }

        public bool UpdateProduct(ProductEntity product)
        {
            // TODO: call data access layer to update product
            // first check to see if it is a valid price
            if (product.UnitPrice <= 0)
                return false;
            // ProductName can't be empty
            else if (product.ProductName.Length == 0)
                return false;
            // QuantityPerUnit can't be empty
            else if (product.QuantityPerUnit.Length == 0)
                return false;
            // then validate other properties
            else
            {
                ProductEntity productInDB =
                GetProduct(product.ProductID);
                // invalid product to update
                if (productInDB == null)
                    return false;
                // a product can't be discontinued if there are non-fulfilled orders
                if (product.Discontinued == true && productInDB.
                UnitsOnOrder > 0)
                    return false;
                else
                    return productDAO.UpdateProduct(product);
            }
        }
    }
}
