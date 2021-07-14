using System;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;

        public CheckOutModel(IBasketService basketService, IOrderService orderService)
        {
            this.basketService = basketService;
            this.orderService = orderService;
        }

        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.GetBasket("swn");
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            Cart = await basketService.GetBasket("swn");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = "swn";
            Order.TotalPrice = Cart.TotalPrice;

            await basketService.CheckoutBasket(Order);
                        
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}