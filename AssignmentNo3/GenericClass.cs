using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNo3
{
    public static class GenericClass<T> where T:class
    {
        public static bool Update(T v)
        {
            using DbEntities entities = new DbEntities();
            try
            {
                entities.Entry(v).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}
