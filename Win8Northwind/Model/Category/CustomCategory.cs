using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win8Northwind.Model.Category
{
    public class CustomCategory : Win8Northwind.Model.Common.ViewModelBase
    {
        public CustomCategory()
        {
        }

        public CustomCategory(CustomCategory other)
        {
            CategoryId = other.CategoryId;
            CategoryName = other.CategoryName;
            Description = other.Description;
        }

        private int _categoryId;
        public int CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (value != _categoryId)
                {
                    _categoryId = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private string _categoryName;
        public string CategoryName 
        {
            get { return _categoryName; } 
	        set
	        {
                if (value != _categoryName)
		        {
                    _categoryName = value;
			        NotifyPropertyChanged();
		        }
	        } 
        }

        private string _description;
        public string Description  
        {
            get { return _description; } 
	        set
	        {
                if (value != _description)
		        {
                    _description = value;
			        NotifyPropertyChanged();
		        }
	        } 
        }

    }
}
