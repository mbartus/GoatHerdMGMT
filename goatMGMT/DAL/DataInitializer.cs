using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using goatMGMT.Models;

using System.Runtime.Remoting.Contexts;

namespace goatMGMT.DAL
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseAlways<DataContext>  {
        
        protected override void Seed(DataContext context)
        {
            base.Seed(context);
            
            // Dummy data, animals
            var animals = new List<Animals>
            {
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-01"),farm_name="Farm 1",name="Goat 1",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-01"),farm_name="Farm 1",name="Goat 2",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-02"),farm_name="Farm 2",name="Goat 3",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-02"),farm_name="Farm 2",name="Goat 4",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-03"),farm_name="Farm 3",name="Goat 5",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-03"),farm_name="Farm 3",name="Goat 6",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-04"),farm_name="Farm 4",name="Goat 7",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-04"),farm_name="Farm 4",name="Goat 8",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-05"),farm_name="Farm 5",name="Goat 9",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Goat",dob=DateTime.Parse("2000-09-05"),farm_name="Farm 5",name="Goat 10",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-01"),farm_name="Farm 1",name="Sheep 1",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-01"),farm_name="Farm 1",name="Sheep 2",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-02"),farm_name="Farm 2",name="Sheep 3",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-02"),farm_name="Farm 2",name="Sheep 4",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-03"),farm_name="Farm 3",name="Sheep 5",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-03"),farm_name="Farm 3",name="Sheep 6",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-04"),farm_name="Farm 4",name="Sheep 7",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-04"),farm_name="Farm 4",name="Sheep 8",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-05"),farm_name="Farm 5",name="Sheep 9",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""},
                new Animals{species="Sheep",dob=DateTime.Parse("2000-10-05"),farm_name="Farm 5",name="Sheep 10",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("2020-09-01"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("2020-09-01"), remarks=""}
            };
            animals.ForEach(s => context.Animals.Add(s));
            context.SaveChanges();

            var children = new List<Children>
            {
                new Children{species="Goat",dob=DateTime.Parse("2005-09-01"),farm_name="Farm 1",name="Goat 1",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Goat",dob=DateTime.Parse("2006-09-15"),farm_name="Farm 1",name="Goat 2",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Goat",dob=DateTime.Parse("2006-09-15"),farm_name="Farm 2",name="Goat 3",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Goat",dob=DateTime.Parse("2007-10-01"),farm_name="Farm 2",name="Goat 4",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Goat",dob=DateTime.Parse("2007-10-01"),farm_name="Farm 3",name="Goat 5",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Goat",dob=DateTime.Parse("2007-10-01"),farm_name="Farm 3",name="Goat 6",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Goat",dob=DateTime.Parse("2012-06-17"),farm_name="Farm 4",name="Goat 7",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Goat",dob=DateTime.Parse("2012-06-17"),farm_name="Farm 4",name="Goat 8",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Goat",dob=DateTime.Parse("2012-06-17"),farm_name="Farm 5",name="Goat 9",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Goat",dob=DateTime.Parse("2012-06-17"),farm_name="Farm 5",name="Goat 10",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2007-01-01"),farm_name="Farm 1",name="Sheep 1",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2008-06-15"),farm_name="Farm 1",name="Sheep 2",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2008-06-15"),farm_name="Farm 2",name="Sheep 3",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2007-03-15"),farm_name="Farm 2",name="Sheep 4",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2007-03-15"),farm_name="Farm 3",name="Sheep 5",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2007-03-15"),farm_name="Farm 3",name="Sheep 6",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2010-01-01"),farm_name="Farm 4",name="Sheep 7",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2010-01-01"),farm_name="Farm 4",name="Sheep 8",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2010-01-01"),farm_name="Farm 5",name="Sheep 9",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='m', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05, market_date=DateTime.Parse("9999-12-30"), remarks=""},
                new Children{species="Sheep",dob=DateTime.Parse("2010-01-01"),farm_name="Farm 5",name="Sheep 10",
                        regulation_no="N/A", breed_code="N/A", microchip_id="N/A", premise_id="N/A", herd_id_code="N/A", 
                        breed_registry="N/A", sex='f', status_code="N/A", disposal_date=DateTime.Parse("9999-12-30"), 
                        current_weight=100.05, market_weight=100.05,market_date=DateTime.Parse("9999-12-30"), remarks=""}
            };
            children.ForEach(s => context.Children.Add(s));
            context.SaveChanges();

            // Births
            var births = new List<Births>
            {
                new Births {mother_id= 1,father_id= 1,date=DateTime.Parse("2005-09-01"),birth_type= 0,birth_parity= 1,remarks="Only one born."},
                new Births {mother_id= 2,father_id= 2,date=DateTime.Parse("2006-09-15"),birth_type= 0,birth_parity= 2,remarks="Two born."},
                new Births {mother_id= 3,father_id= 3,date=DateTime.Parse("2007-10-01"),birth_type= 0,birth_parity= 3,remarks="Three born."},
                new Births {mother_id= 4,father_id= 4,date=DateTime.Parse("2012-06-17"),birth_type= 0,birth_parity= 4,remarks="Four born."},
                new Births {mother_id= 5,father_id= 5,date=DateTime.Parse("2007-01-01"),birth_type= 0,birth_parity= 1,remarks="Only one born."},
                new Births {mother_id= 6,father_id= 6,date=DateTime.Parse("2008-06-15"),birth_type= 0,birth_parity= 2,remarks="Two born."},
                new Births {mother_id= 7,father_id= 7,date=DateTime.Parse("2007-03-15"),birth_type= 0,birth_parity= 3,remarks="Three born."},
                new Births {mother_id= 8,father_id= 8,date=DateTime.Parse("2010-01-01"),birth_type= 0,birth_parity= 4,remarks="Four born."}
            };
            births.ForEach(s => context.Births.Add(s));
            context.SaveChanges();

            // Breedings
            var breedings = new List<Breedings>
            {
                new Breedings {mother_id=1, father_id=1, date=DateTime.Parse("2005-04-01"), remarks=""},
                new Breedings {mother_id=2, father_id=2, date=DateTime.Parse("2006-04-15"), remarks=""},
                new Breedings {mother_id=3, father_id=3, date=DateTime.Parse("2007-05-01"), remarks=""},
                new Breedings {mother_id=4, father_id=4, date=DateTime.Parse("2012-02-17"), remarks=""},
                new Breedings {mother_id=5, father_id=5, date=DateTime.Parse("2006-08-01"), remarks=""},
                new Breedings {mother_id=6, father_id=6, date=DateTime.Parse("2008-01-15"), remarks=""},
                new Breedings {mother_id=7, father_id=7, date=DateTime.Parse("2006-12-15"), remarks=""},
                new Breedings {mother_id=8, father_id=8, date=DateTime.Parse("2009-08-01"), remarks=""}
            };
            breedings.ForEach(s => context.Breedings.Add(s));
            context.SaveChanges();

            // Treatments
            var treatments = new List<Treatments>
            {

            };
            treatments.ForEach(s => context.Treatments.Add(s));
            context.SaveChanges();

            // Animal Treatments
            var animal_treamnets = new List<AnimalTreatments>
            {

            };
            animal_treamnets.ForEach(s => context.AnimalTreatments.Add(s));
            context.SaveChanges();

            // Associates
            var associates = new List<Associates>
            {

            };
            associates.ForEach(s => context.Associates.Add(s));
            context.SaveChanges();

            // Transactions
            var transactions = new List<Transactions>
            {

            };
            transactions.ForEach(s => context.Transactions.Add(s));
            context.SaveChanges();
        }
    }
}