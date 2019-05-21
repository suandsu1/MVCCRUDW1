namespace MVCCRUDW1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(View客戶統計表MetaData))]
    public partial class View客戶統計表
    {
    }
    
    public partial class View客戶統計表MetaData
    {
        public int Id { get; set; }
        public string 客戶名稱 { get; set; }
        public Nullable<bool> 刪除 { get; set; }
        public Nullable<int> 銀行帳戶數量 { get; set; }
        public Nullable<int> 聯絡人數量 { get; set; }
    }
}
