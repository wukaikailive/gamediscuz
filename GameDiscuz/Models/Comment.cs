//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameDiscuz.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int CommentID { get; set; }
        public Nullable<int> PostID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string PostContent { get; set; }
        public Nullable<System.DateTime> CommentDate { get; set; }
        public Nullable<int> CommentStatus { get; set; }
    
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
