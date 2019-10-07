using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Category:IEntity
    {
        public List<Product> Products { get; set; }
        public List<Category> SubCategories { get; set; }

        private int _CategoryId;

        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        private int _ParentId;
        public int ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }

        private string _CategoryName;

        [Required(ErrorMessage = "Category ismi boş geçilemez")]
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private string _Description;
        [Required(ErrorMessage = "Category Açıklaması boş geçilemez")]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _MetatagTitle;

        public string MetatagTitle
        {
            get { return _MetatagTitle; }
            set { _MetatagTitle = value; }
        }

        private string _MetatagDescription;

        public string MetatagDescription
        {
            get { return _MetatagDescription; }
            set { _MetatagDescription = value; }
        }

        private string _Metatagkeywords;

        public string Metatagkeywords
        {
            get { return _Metatagkeywords; }
            set { _Metatagkeywords = value; }
        }

        private string _ImageUrl;

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        private bool _ShowNavbar;

        public bool ShowNavbar
        {
            get { return _ShowNavbar; }
            set { _ShowNavbar = value; }
        }


        private bool _isActive;

        public bool isActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
    }
}