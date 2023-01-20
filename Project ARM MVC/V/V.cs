using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_ARM_MVC.Models
{
    public partial class VUser
    {
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "ชื่อผู้ใช้")]
        public string User_Name { get; set; }
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "รหัสผู้ใช้")]
        public string User_Pass { get; set; }
     
        
        
    }
    [MetadataType(typeof(VUser))]
    public partial class User { }
  
    /// ///////////////////////////////////////////
   
    public partial class VCategory
    {
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "ชื่อเรื่อง")]
        public string Category_Data { get; set; }
        
       
        
    }
    [MetadataType(typeof(VCategory))]
    public partial class Category { }


    public partial class VAdmin
    {
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "ชื่อผู้ควบคุมเว็บ")]
        public string Admin_Name { get; set; }
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "รหัสผู้ควบคุมเว็บ")]
        public string Admin_Pass { get; set; }


    }
    [MetadataType(typeof(VAdmin))]
    public partial class Admin { }





    public partial class VContent
    {
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "ชื่อเรื่อง")]
        public Nullable<int> Content_CategoryID { get; set; }
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "ชื่อเนื้อหา")]
        public string Content_Name { get; set; }
        



    }
    [MetadataType(typeof(VContent))]
    public partial class Content { }



    public partial class VWContent
    {


        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "ชื่อเนื้อหา")]
        public Nullable<int> WContent_ContentID { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "ชื่อเนื้อหาย่อ")]
        public string WContent_Name { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        [Display(Name = "ข้อมูล")]
        public string WContent_Data { get; set; }



    }
    [MetadataType(typeof(VWContent))]
    public partial class WContent { }




    public partial class VWStudies
    {

        public Nullable<int> Study_UserId { get; set; }
        public Nullable<int> Study_WcontentID { get; set; }
    }
    [MetadataType(typeof(VWStudies))]
    public partial class Studies { }





}