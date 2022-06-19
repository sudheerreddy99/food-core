using foodcore.Models;
using System.Linq;
using System.Collections.Generic;

namespace foodcore
{
    public interface myinter
    {
        List<Menu> DisplayFoodItem();

    }
    public class foodcorecls : myinter
    {
        FoodcoreContext dc = new FoodcoreContext();

        public List<Menu> DisplayFoodItem()
        {


            var result = dc.Menus.ToList();
            return result;
        }

    }
}
