using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyWCFServices.LINQNorthwindEntities;
using System.Data.SqlClient;
using System.Configuration;
using LINQNorthwindDAL;

namespace MyWCFServices.LINQNorthwindDAL
{
    public class ProductDAO
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
        public ProductEntity GetProduct(int id)
        {
            NorthwindEntities NWEntities = new NorthwindEntities();
            Product product = (from p in NWEntities.Products
                               where p.ProductID == id
                               select p).FirstOrDefault();
            ProductEntity productEntity = null;
            if (product != null)
            {
                productEntity = new ProductEntity()
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = (decimal)product.UnitPrice,
                    UnitsInStock = (int)product.UnitsInStock,
                    ReorderLevel = (int)product.ReorderLevel,
                    UnitsOnOrder = (int)product.UnitsOnOrder,
                    Discontinued = product.Discontinued,
                    RowVersion = product.RowVersion
                };
            }
            NWEntities.Dispose();
            return productEntity;
        }

        public bool UpdateProduct(ref ProductEntity productEntity)
        {
            // check product ID
            NorthwindEntities NWEntities = new NorthwindEntities();
            // save productID in a variable
            int productID = productEntity.ProductID;
            Product productInDB =
            (from p
            in NWEntities.Products
             where p.ProductID == productID
             select p).FirstOrDefault();
            // check product
            if (productInDB == null)
            {
                throw new Exception("No product with ID " +
                productEntity.ProductID);
            }

            // first detach the object
            NWEntities.Detach(productInDB);

            // update product price
            productInDB.ProductName = productEntity.ProductName;
            productInDB.QuantityPerUnit = productEntity.QuantityPerUnit;
            productInDB.UnitPrice = productEntity.UnitPrice;
            productInDB.Discontinued = productEntity.Discontinued;
            productInDB.RowVersion = productEntity.RowVersion;

            // now attach the object again
            NWEntities.Attach(productInDB);
            NWEntities.ObjectStateManager.ChangeObjectState(productInDB, System.Data.EntityState.Modified);


            NWEntities.SaveChanges();
            //refresh the RowVersion property
            productEntity.RowVersion = productInDB.RowVersion;
            NWEntities.Dispose();
            return true;
        }
    }
}
