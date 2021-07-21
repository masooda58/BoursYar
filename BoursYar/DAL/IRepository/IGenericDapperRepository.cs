using System.Collections.Generic;

namespace DAL
{ 
   public interface IGenericDapperRepository<TEntity> where TEntity:class
   {
        // get data from TEntity  where = filter
        IEnumerable<TEntity> GetData(object filter);
        // get query select where  parmeter"><=" @parameter ....
       IEnumerable<TEntity>  GetData(string qry, object parameters);
        // get all
        IEnumerable<TEntity> GetAllData();
        void AddData(TEntity entity);
        void DeleteData(TEntity entity);
        void AddDataList(List<TEntity> entities);
        //   void UpdateData(TEntity entity);
        void DeleteAllData();
       void DeleteData(string qry, object parameters);
        void UpdateData(TEntity entity);
        void UpdateDataList(List<TEntity> entities);
        // delete with where  parameter == key
       void Remove(object key);
       IEnumerable<TEntity> GetQureyData(string qry);



   }
}
