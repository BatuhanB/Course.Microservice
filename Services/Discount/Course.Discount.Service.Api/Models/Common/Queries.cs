namespace Course.Discount.Service.Api.Models.Common
{
    public static class Queries
    {
        public static string GetAllDiscount => @$"Select * from discount.discount";
        public static string GetDiscountById => @$"Select * from discount.discount where id = @id";
        public static string GetDiscountByCodeAndUserId => @$"Select * from discount.discount where code = @code and userid = @userId";
        public static string CreateDiscount => @$"Insert into discount.discount(userid,rate,code) Values(@userid,@rate,@code)";
        public static string UpdateDiscount => @$"Update discount.discount set userid=@userid,rate=@rate,code=@code where id=@id";
        public static string DeleteDiscount => @$"Delete from discount.discount where id=@id";
    }
}
