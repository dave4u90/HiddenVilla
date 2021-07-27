using System;
namespace Common
{
    public static class SD
    {
        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";
        public const string Role_Employee = "Employee";

        public const string Local_InitialRoomBooking = "InitialRoomBookingInfo";
        public const string Local_RoomOrderDetails = "RoomOrderDetails";
        public const string Local_Token = "JWT Token";
        public const string Local_UserDetails = "User Details";

        public const string Status_Pending = "Pending";
        public const string Status_Booked = "Booked";
        public const string Status_CheckedIn = "CheckedIn";
        public const string Status_CheckedOut_Completed = "CheckedOut";
        public const string Status_NoShow = "NoShow";
        public const string Status_Cancelled = "Cancelled";

        public const string Stripe_Paid = "paid";
        public const string Stripe_Unpaid = "unpaid";
        public const string Stripe_No_Payment_Required = "no_payment_required";
    }
}
