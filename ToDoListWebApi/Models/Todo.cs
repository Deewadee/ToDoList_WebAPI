using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebApi
{
    public class Todo
    {
        public Todo()
        {
        }
        public int TodoId
        {
            get
            {
                return _todoId;
            }
            set
            {
                _todoId = value;
            }
        }

        public string TodoDescription
        {
            get
            {
                return CheckValue(_todoDescription);
            }
            set
            {
                _todoDescription = CheckValue(value);
            }
        }

        public string TodoStatus
        {
            get
            {
                return CheckValue(_todoStatus);
            }
            set
            {
                _todoStatus = CheckValue(value);
            }
        }

        public DateTime TodoDate
        {
            get
            {
                return _todoDate;
            }
            set
            {
                _todoDate = value;
            }
        }


        private string CheckValue(string value)
        {
            return string.IsNullOrEmpty(value) ? "" : value;
        }

        private int _todoId;
        private string _todoDescription;
        private string _todoStatus;
        private DateTime _todoDate;
    }
}
