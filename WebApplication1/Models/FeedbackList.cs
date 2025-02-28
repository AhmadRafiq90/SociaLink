using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Models
{
    public class FeedbackList
    {
        private readonly List<Feedback> _feedbacks;

        public FeedbackList()
        {
            // read_Date()
        }


        void read_Date()
        {
            // store feedback in _feedbacks
        }

        public List<Feedback> GetFeedbacksBySocietyId(string societyId)
        {
            // Return feedback entries that match the given society ID
            return _feedbacks
                .Where(f => f.society_id == societyId)
                .ToList();
        }

    }
}
