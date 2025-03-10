using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;
using System;
using System.Threading.Tasks;
using WebApplication1.DBcontext;
namespace WebApplication1.Pages
{
    public class student_feebackModel : PageModel
    {

        public string SocietyId { get; set; }
        public string SocietyName { get; set; }


        public IActionResult OnGet(string societyId)
        {
            var factory = new FASTSocietyManagementContextFactory();
            var context = factory.GetContext();
            var society = context.Societies.Find(societyId);
            SocietyId = societyId;
            SocietyName = society.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var factory = new FASTSocietyManagementContextFactory();
            var context = factory.GetContext();

            // Extracting radio button values
            var satisfactionValue = Request.Form["satisfactionRating"];
            var preferenceValue = Request.Form["preferenceRating"];
            var skillDevelopmentValue = Request.Form["skillDevelopmentRating"];
            var communicationEffectivenessValue = Request.Form["communicationEffectivenessRating"];
            var professionalDevelopmentValue = Request.Form["professionalDevelopmentRating"];

            // Mapping radio button values to numeric ratings
            var satisfactionRating = MapRatingValue(satisfactionValue);
            var preferenceRating = MapRatingValue(preferenceValue);
            var skillDevelopmentRating = MapRatingValue(skillDevelopmentValue);
            var communicationEffectivenessRating = MapRatingValue(communicationEffectivenessValue);
            var professionalDevelopmentRating = MapRatingValue(professionalDevelopmentValue);

            // Calculate average rating out of 5
            var totalRatings = 5; // Assuming 5 criteria for rating
            var sumOfRatings = satisfactionRating + preferenceRating + skillDevelopmentRating +
                               communicationEffectivenessRating + professionalDevelopmentRating;
            var averageRating = (float)sumOfRatings / totalRatings;

            // Create a feedback object
            var feedback = new Feedback
            {
                SocietyId = SocietyId,
                StudentId = Request.Form["studentId"],
                Comments = Request.Form["comments"],
                AverageRating = averageRating, // Save the average rating to the database
                FeedbackDate = DateTime.Now
            };

            try
            {
                await context.Feedbacks.AddAsync(feedback); // Assuming "Add" method to add feedback to the database
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving feedback: {ex.Message}");
                return Page();
            }

            return RedirectToPage("/Index"); // Redirect to homepage after submission
        }

        // Helper method to map radio button value to numeric rating
        private int MapRatingValue(string ratingValue)
        {
            // Implement your logic to map radio button values to numeric ratings here
            switch (ratingValue)
            {
                case "mostSatisfied":
                    return 5;
                case "satisfied":
                    return 4;
                case "noComment":
                    return 3;
                case "notSatisfied":
                    return 2;
                default:
                    return 1;
            }
        }

    }
}
