﻿
namespace BackOffice.Dbo
{
    public partial class GroupUser : Utils.BaseEntity<int>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}