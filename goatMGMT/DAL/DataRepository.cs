using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using goatMGMT.Models;

namespace goatMGMT.DAL
{
    public class DataRepository
    {
        public List<Animals> GetAnimals()
        {
            DataContext dataContext = new DataContext();
            return dataContext.Animals.ToList();
        }
    }
}