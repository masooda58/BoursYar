using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper;
using Dapper.Contrib.Extensions;
using MoralesLarios.Data.Helper;

namespace DAL
{
  
    public class GenericDapperRepository<TEntity>:IGenericDapperRepository<TEntity> where TEntity:class
   {
       
       private IDbConnection _db;
       public PartsQryGenerator<TEntity> partsQryGenerator { private get; set; }
       private char ParameterIdentified { get; set; }

        public GenericDapperRepository(IDbConnection db)
       {

         
               _db = db;
               if (_db.State == ConnectionState.Closed)
               {
                   _db.Open();
               }
               ParameterIdentified = '@';
               partsQryGenerator = new PartsQryGenerator<TEntity>(ParameterIdentified);
          
        
          
        
        }
        public IEnumerable<TEntity> GetData(object filter)
        {
            //https://www.codeproject.com/Articles/1186566/Dapper-Generic-Repository
            //MoralesLarios.Data/MoralesLarios.Data.Dapper/Infrastructure/
            // ParameterValidator from MoralesLarios.Data.Helper
            // C:\Stock-Project\WinForm\WDbManager\packages

            ParameterValidator.ValidateObject(filter, nameof(filter));

            var selectQry = partsQryGenerator.GenerateSelect(filter);
            var result = _db.Query<TEntity>(selectQry, filter);

            return result;
        }

        public IEnumerable<TEntity> GetData(string qry, object parameters)
        {
            ParameterValidator.ValidateString(qry, nameof(qry));
            ParameterValidator.ValidateObject(parameters, nameof(parameters));

            var result = _db.Query<TEntity>(qry, parameters);

            return result;
        }

        public IEnumerable<TEntity> GetAllData()
        {
            return _db.GetAll<TEntity>().ToList();
        }

        public void AddData(TEntity entity)
        {
            var x = _db.Insert(entity);
        }

        public void AddDataList(List<TEntity> entities)
        {
            var x = _db.Insert(entities);
        }

        public void DeleteData(TEntity entity)
        {
            var x = _db.Delete(entity);
        }

        //public void UpdateData(TEntity entity)
        //{
        //    _db.UpdateAsync(entity);
        //}

        public void DeleteAllData()
        {
           
          var c=  _db.DeleteAllAsync<TEntity>();
        }

       public void DeleteData(string qry, object parameters)
       {
           _db.Execute(qry, parameters);
        }

       public void Remove(object key)
       {
           ParameterValidator.ValidateObject(key, nameof(key));

           var deleteQry = partsQryGenerator.GenerateDelete(key);

           _db.Execute(deleteQry, key);
       }

        public void UpdateData(TEntity entity)
       {
           _db.Update(entity);
       }

       public void UpdateDataList(List<TEntity> entities)
       {
           _db.Update(entities);
       }
        public IEnumerable<TEntity> GetQureyData(string qry, object parameters)
        {

            //string qry = "SELECT * FROM EMPLOYEES WHERE AGE > @Age AND INCOMES > @Incomes";

            //object parameters = new { Age = 30, Incomes = 35000 };

            //var employeesMore30yearsMore35000 = employeesRepository.GetData(qry, parameters);
            ParameterValidator.ValidateString(qry, nameof(qry));
            ParameterValidator.ValidateObject(parameters, nameof(parameters));

            var result = _db.Query<TEntity>(qry, parameters);

            return result;
        }
        public IEnumerable<TEntity> GetQureyData(string qry)
        {

            //string qry = "SELECT * FROM EMPLOYEES WHERE AGE > @Age AND INCOMES > @Incomes";

            //object parameters = new { Age = 30, Incomes = 35000 };

            //var employeesMore30yearsMore35000 = employeesRepository.GetData(qry, parameters);
            //ParameterValidator.ValidateString(qry, nameof(qry));
            //ParameterValidator.ValidateObject(parameters, nameof(parameters));

            var result = _db.Query<TEntity>(qry);

            return result;
        }
    }
  
  
}
