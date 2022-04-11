using Fora.UI.Data;
using Fora.UI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Fora.UI.Pages.Forum
{
    [BindProperties]
    public class SignupModel : PageModel
    {
        private readonly AppDbContextUI _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public RegistrationModel Signup { get; set; }
        public UserModel User { get; set; } 

       [Required]
       [Display(Name = "Create new interest")]
        public string NewInterestName { get; set; }

        public SignupModel(SignInManager<IdentityUser> signInManager, AppDbContextUI context)
        {
            _signInManager = signInManager;
            _context = context;
            User = new UserModel();
        }

      
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //await _signInManager.SignOutAsync();
            User.Username = Signup.Username;

            await _context.AddAsync(User);
            await _context.SaveChangesAsync();


            if (ModelState.IsValid)
            {
                // Registration
                IdentityUser newuser = new IdentityUser();
               
                newuser.UserName = Signup.Username;
                newuser.Email = Signup.Email;


                IdentityResult registration = await _signInManager.UserManager.CreateAsync(newuser, Signup.Password);

                if (registration.Succeeded)
                {
               
                    // Logga in användare
                    var signInResult = await _signInManager.PasswordSignInAsync(Signup.Username, Signup.Password, false, false);

                    if (signInResult.Succeeded)
                    {

                        if (!string.IsNullOrEmpty(NewInterestName))
                        {
                            var currentUser = await _signInManager.UserManager.FindByNameAsync(User.Username); // Use the FindByNameAsync(String) method if not signed in
                            var user = _context.Users.FirstOrDefault(u => u.Username == currentUser.UserName); // Get the corresponding user in the other db

                            if (user != null)
                            {
                               
                                await new InterestManager(_context).CreateInterestAsync(new InterestModel () // Create interest
                                {
                                    Name = NewInterestName,
                                    User = user
                                });

                                var interestToAdd = _context.Interests.Where(x => x.Name == NewInterestName).FirstOrDefault();

                                if (interestToAdd != null)
                                {
                                    _context.UserInterests.Add(new UserInterestModel()
                                    {
                                        Interest = interestToAdd,
                                        User = user
                                    });
                                   
                                }
                                _context.SaveChanges();
                                
                            }
                            // Omdirigera till välkomstsidan
                            return RedirectToPage("/Forum/Threads");
                        }
                    }
                
                }
            

             }
        return Page();
         
        }
    }
}
