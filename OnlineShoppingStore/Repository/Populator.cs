using OnlineShopEdmx;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopping.Repository
{
    public class Populator
    {
        public List<PairModel> GetPairModel(string param, long param1 = 0, long param2 = 0)
        {
            List<PairModel> _pairModel = new List<PairModel>();

            switch (param)
            {
                case "IsActive":
                    _pairModel.Add(new PairModel { Key = true, Value = "Active" });
                    _pairModel.Add(new PairModel { Key = false, Value = "In-active" });
                    return _pairModel;
                case "Gender":
                    _pairModel.Add(new PairModel { Key = true, Value = "Male" });
                    _pairModel.Add(new PairModel { Key = false, Value = "Female" });
                    return _pairModel;
                default:
                    return _pairModel;
            }

        }
        public List<IntStringPairModel> GetIntStringPairModel(string param, long param1 = 0, long param2 = 0)
        {
            List<IntStringPairModel> _pairModel = new List<IntStringPairModel>();
            eCommerceEntities db = new eCommerceEntities();
            switch (param)
            {
                case "ProductFeature":
                    _pairModel = db.ProductFeature.Where(x => x.IsActive == true ).Select(x => new IntStringPairModel { Key = x.ProductFeatureId, Value = (x.ProductFeatureName) }).ToList<IntStringPairModel>();
                    return _pairModel;

                case "Category":
                    _pairModel = db.Category.Where(x => x.IsActive == true).Select(x => new IntStringPairModel { Key = x.CategoryId, Value = (x.CategoryName) }).ToList<IntStringPairModel>();
                    return _pairModel;

                default:
                    return _pairModel;
            }
        }

    }
}