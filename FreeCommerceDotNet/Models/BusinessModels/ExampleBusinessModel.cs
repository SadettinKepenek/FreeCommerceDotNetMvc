using FreeCommerceDotNet.Models.DbManager;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ExampleBusinessModel
    {
        public ExampleBusinessModel(int primaryKey)
        {
            // param primaryKey: Primary Key Column Value of Business Model
            // Primary Element
            using (ProductManager manager = new ProductManager())
            {
            }

            /// Foreign Keys
            using (ProductAttributeManager manager = new ProductAttributeManager())
            {
            }
            using (ProductDiscountManager manager = new ProductDiscountManager())
            {
        
            }
            using (ProductImageManager manager = new ProductImageManager())
            {
        
            }
            using (ProductOptionsManager manager = new ProductOptionsManager())
            {
                
            }
            using (ProductPriceManager manager = new ProductPriceManager())
            {
                
            }
            using (ReviewsManager manager = new ReviewsManager())
            {

            }
        }
    }
}