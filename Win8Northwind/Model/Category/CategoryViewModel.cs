using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http.Headers;
using Win8Northwind.Model.Common;
using Windows.UI.Xaml.Controls.Primitives;

namespace Win8Northwind.Model.Category
{
    public class CategoryViewModel : Win8Northwind.Model.Common.ViewModelBase
    {
        private const string apiRoot = "http://webapi.nw.vncvr.ca/api/Category";

        //private const string apiRoot = "http://localhost:3525/api/Category";

        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        
        private bool _editing; 
        public bool Editing 
        {
            get { return _editing; } 
	        set
	        {
                if (value != _editing)
		        {
                    _editing = value;
			        NotifyPropertyChanged();
		        }
	        } 
        }

        private CustomCategory _editedCategory;
        public CustomCategory EditedCategory 
        {
            get { return _editedCategory; } 
	        set
	        {
                if (value != _editedCategory)
		        {
                    _editedCategory = value;
			        NotifyPropertyChanged();
		        }
	        } 
        }

        private CustomCategory _selected;
        public CustomCategory Selected
        { 
	        get { return _selected; } 
	        set
	        {
		        if (value != _selected)
		        {
			        _selected = value;
			        NotifyPropertyChanged();
		        }
	        } 
        }

        private ObservableCollection<CustomCategory> _customCategories;
        public ObservableCollection<CustomCategory> CustomCategories
        {
            get { return _customCategories; }
            set
            {
                if (value != _customCategories)
                {
                    _customCategories = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public CategoryViewModel()
        {
            AddCommand = new DelegateCommand(AddPerson);
            //AddCommand = new DelegateCommand(Add);
            EditCommand = new DelegateCommand(EditPerson);
            DeleteCommand = new DelegateCommand(DeletePerson);

            if (IsDesignMode)
            {
                Load();
                Editing = true;
            }
        }


        /* this works too */
        async private void Add(object obj)
        {
            //Get new client information
            CustomCategory newcat = new CustomCategory() {
                    CategoryName = "POP",
                    Description = "COKE"
            };
                


            //create httpContent for the HttpResponceMessage that will go to Asp.net Web API 
            HttpContent content = new StringContent(Utils.Serialize(newcat));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //send client to webApi using postasync method 
            HttpClient _client = new HttpClient { BaseAddress = new Uri("http://localhost:3525/") }; 
            HttpResponseMessage responceMessage =  _client.PostAsync("api/category", content).Result as HttpResponseMessage;

            //here we get responce in single object not a collection 

            //here sample code for geting result as JsonString and Deserialize in Object 
            string objResult = responceMessage.Content.ReadAsStringAsync().Result;
            var newitem = Utils.Deserialize<CustomCategory>(objResult);
           
            string statusCode = responceMessage.StatusCode.ToString();

        }

        async private void DeletePerson(object obj)
        {
            /* for this to work, put this XML in the <system.webServer> section of the web.config file
            <modules runAllManagedModulesForAllRequests="true" />
             */

            if (Selected != null)
            {
                if (!IsDesignMode)
                {
                    var http = new HttpClient();
                    string url = apiRoot + "/" + Selected.CategoryId;
                    var resp = await http.DeleteAsync(new Uri(url));
                    resp.EnsureSuccessStatusCode();

                    /* this works too .....*/
                    //HttpClient _client = new HttpClient { BaseAddress = new Uri("http://localhost:3525/") };
                    //var responceMessage = _client.DeleteAsync(string.Format("api/category/{0}", Selected.CategoryId)).Result as HttpResponseMessage;
                    //responceMessage.EnsureSuccessStatusCode();
                    //string message = responceMessage.Content.ReadAsStringAsync().Result;

                }
                CustomCategories.Remove(Selected);
            }
        }

        private void EditPerson(object obj)
        {
            EditedCategory = new CustomCategory(Selected);
            Editing = true;
        }

        private void AddPerson(object obj)
        {
            EditedCategory = new CustomCategory();
            Editing = true;
        }

        async public void EditPersonCommit()
        {
            var editedCategory = EditedCategory;
            var original = CustomCategories.SingleOrDefault(p => p.CategoryId == editedCategory.CategoryId);

            if (!IsDesignMode)
            {
                var http = new HttpClient();
                var djs = new DataContractJsonSerializer(typeof(CustomCategory));
                var ms = new MemoryStream();
                djs.WriteObject(ms, editedCategory);
                ms.Position = 0;
                var sc = new StreamContent(ms);
                sc.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var resp = original == null ?
                        await http.PostAsync(new Uri(apiRoot), sc) :
                        await http.PutAsync(new Uri(apiRoot + "/" + original.CategoryId), sc);

                ms.Dispose();
                resp.EnsureSuccessStatusCode();
            }

            if (original != null)
            {
                original.CategoryName = editedCategory.CategoryName;
                original.Description = editedCategory.Description;
            }
            else
            {
                CustomCategories.Add(editedCategory);
            }
        }

        async public void Load()
        {
            if (IsDesignMode)
            {
                CustomCategories = new ObservableCollection<CustomCategory>
                {
                    new CustomCategory { CategoryId=1111, CategoryName = "design fred", Description = "Nurse" },
                    new CustomCategory { CategoryId=2222, CategoryName = "design bob", Description = "Artist" },
                    new CustomCategory { CategoryId=3333, CategoryName = "design flo", Description = "Teacher" },
                    new CustomCategory { CategoryId=4444, CategoryName = "design zak", Description = "Fireman" },
                    new CustomCategory { CategoryId=5555, CategoryName = "design rita", Description = "Nurse" },
                };
            }
            else
            {
                var http = new HttpClient();
                var resp = await http.GetAsync(new Uri(apiRoot));
                var stream = await resp.Content.ReadAsStreamAsync();
                var djs = new DataContractJsonSerializer(typeof(List<CustomCategory>));
                CustomCategories = new ObservableCollection<CustomCategory>((IEnumerable<CustomCategory>)djs.ReadObject(stream));
                stream.Dispose();
            }
        }
    }
}
