using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Utility;

namespace CBTD.Pages.OrderConfirmation;

public class OrderConfirmation : PageModel
{
    [BindProperty]
    public int OrderId { get; set; }
    private readonly UnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    public OrderConfirmation(UnitOfWork unitOfWork, IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
    }
    public void OnGet(int orderId)
    {
        OrderHeader objOrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includes: "ApplicationUser");
        OrderId = objOrderHeader.Id;
        // var service = new SessionService();
        // Session session = service.Get(objOrderHeader.SessionId);
        // //check the stripe status
        // if (session.PaymentStatus.ToLower() == "paid")
        // {
        //     _unitOfWork.OrderHeader.UpdateStatus(orderId, OrderStatus.StatusApproved, OrderStatus.PaymentStatusApproved);
        //     _unitOfWork.Commit();
        // }
        //Send an e-mail

        _emailSender.SendEmailAsync(objOrderHeader.ApplicationUser.Email, "New Order - CBTD", "<p>New Order Created.</p>Your order number is " + OrderId.ToString());
			
        //remove shopping cart items
        List<ShoppingCartItem> shoppingCartItems = _unitOfWork.ShoppingCartItem.GetAll(u => u.ApplicationUserId ==
                                                                                    objOrderHeader.ApplicationUserId).ToList();
        _unitOfWork.ShoppingCartItem.Delete(shoppingCartItems);
        _unitOfWork.Commit();           
    }
}

