using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controller
{
    public class DataStorage<T>
    {
        private List<T> _list = new List<T>();
        public void Add(T input)
        {
            _list.Add(input);
        }


    }
}
