using Microsoft.AspNetCore.Components;

namespace LiveMade.Views.Components.Surveys
{
    public partial class SurveyComponent : ComponentBase
    {
        public class TalkView
        {
            public int Id { get; set; }
            public string Track { get; set; } = string.Empty;
            public string Time { get; set; } = string.Empty;
            public string Room { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Speaker { get; set; } = string.Empty;
            public string SpeakerAvatarUrl { get; set; } = string.Empty;

            public double AverageRating { get; set; }
            public bool HasAverageRating { get; set; }

            public double UserRating { get; set; }
            public string LastComment { get; set; } = string.Empty;
            public DateTime LastRatedAt { get; set; }
        }

        public class RecentRatingView
        {
            public int TalkId { get; set; }
            public string Title { get; set; } = string.Empty;
            public double Rating { get; set; }

            public string TruncatedTitle
            {
                get
                {
                    if (Title.Length <= 18)
                    {
                        return Title;
                    }

                    return Title.Substring(0, 16) + "...";
                }
            }
        }

        protected List<TalkView> Talks { get; set; } = new List<TalkView>();

        protected string SelectedTrackFilter { get; set; } = "All Tracks";

        protected bool IsModalOpen { get; set; }
        protected int CurrentRating { get; set; }
        protected string RatingLabel { get; set; } = "Click to rate";
        protected string CommentText { get; set; } = string.Empty;

        protected string CurrentTalkTitle { get; set; } = string.Empty;
        protected string CurrentSpeakerName { get; set; } = string.Empty;
        protected int CurrentTalkId { get; set; }

        protected override void OnInitialized()
        {
            Talks = new List<TalkView>
            {
                new TalkView
                {
                    Id = 1,
                    Track = "Technical",
                    Time = "09:00 - 10:00 AM",
                    Room = "Main Hall",
                    Title = "Building Scalable Microservices Architecture",
                    Description = "Learn how to design and implement microservices that can handle millions of requests with proper load balancing and fault tolerance.",
                    Speaker = "Dr. Sarah Chen",
                    SpeakerAvatarUrl = "https://storage.googleapis.com/uxpilot-auth.appspot.com/avatars/avatar-2.jpg",
                    AverageRating = 4.2,
                    HasAverageRating = true
                },
                new TalkView
                {
                    Id = 2,
                    Track = "Business",
                    Time = "10:30 - 11:30 AM",
                    Room = "Room A",
                    Title = "The Future of Remote Work and Team Collaboration",
                    Description = "Exploring innovative tools and strategies that are reshaping how distributed teams collaborate and maintain productivity.",
                    Speaker = "Michael Rodriguez",
                    SpeakerAvatarUrl = "https://storage.googleapis.com/uxpilot-auth.appspot.com/avatars/avatar-3.jpg",
                    HasAverageRating = false
                },
                new TalkView
                {
                    Id = 3,
                    Track = "Design",
                    Time = "01:00 - 02:00 PM",
                    Room = "Design Studio",
                    Title = "AI-Powered Design Tools: Revolution or Evolution?",
                    Description = "A deep dive into how artificial intelligence is transforming the design process and what it means for creative professionals.",
                    Speaker = "Emma Thompson",
                    SpeakerAvatarUrl = "https://storage.googleapis.com/uxpilot-auth.appspot.com/avatars/avatar-5.jpg",
                    AverageRating = 4.8,
                    HasAverageRating = true
                },
                new TalkView
                {
                    Id = 4,
                    Track = "Technical",
                    Time = "02:30 - 03:30 PM",
                    Room = "Tech Lab",
                    Title = "Quantum Computing: From Theory to Practice",
                    Description = "Understanding the fundamentals of quantum computing and exploring real-world applications in various industries.",
                    Speaker = "Prof. David Kim",
                    SpeakerAvatarUrl = "https://storage.googleapis.com/uxpilot-auth.appspot.com/avatars/avatar-8.jpg",
                    HasAverageRating = false
                }
            };
        }

        protected IEnumerable<TalkView> FilteredTalks
        {
            get
            {
                if (SelectedTrackFilter == "All Tracks")
                {
                    return Talks;
                }

                return Talks.Where(talk => talk.Track == SelectedTrackFilter);
            }
        }

        protected int TotalTalks
        {
            get { return Talks.Count; }
        }

        protected int RatedTalks
        {
            get { return Talks.Count(talk => talk.UserRating > 0); }
        }

        protected string ProgressPercentString
        {
            get
            {
                if (TotalTalks == 0)
                {
                    return "0%";
                }

                double percent = (double)RatedTalks / TotalTalks * 100.0;
                if (percent < 0.0)
                {
                    percent = 0.0;
                }

                if (percent > 100.0)
                {
                    percent = 100.0;
                }

                return percent.ToString("0.##") + "%";
            }
        }

        protected List<RecentRatingView> RecentRatings
        {
            get
            {
                List<RecentRatingView> items = Talks
                    .Where(talk => talk.UserRating > 0)
                    .OrderByDescending(talk => talk.LastRatedAt)
                    .Take(5)
                    .Select(talk => new RecentRatingView
                    {
                        TalkId = talk.Id,
                        Title = talk.Title,
                        Rating = talk.UserRating
                    })
                    .ToList();

                return items;
            }
        }

        protected void OpenRatingModal(TalkView talk)
        {
            CurrentTalkId = talk.Id;
            CurrentTalkTitle = talk.Title;
            CurrentSpeakerName = talk.Speaker;
            CommentText = string.Empty;

            TalkView existing = Talks.First(t => t.Id == talk.Id);

            if (existing.UserRating > 0)
            {
                CurrentRating = (int)Math.Round(existing.UserRating);
                RatingLabel = GetRatingLabel(CurrentRating);
            }
            else
            {
                ResetRatingInternal();
            }

            IsModalOpen = true;
        }

        protected void CloseRatingModal()
        {
            IsModalOpen = false;
            ResetRatingInternal();
            CommentText = string.Empty;
        }

        protected void SetRating(int rating)
        {
            if (rating < 1)
            {
                rating = 1;
            }

            if (rating > 5)
            {
                rating = 5;
            }

            CurrentRating = rating;
            RatingLabel = GetRatingLabel(rating);
        }

        protected void SubmitRating()
        {
            if (CurrentRating == 0)
            {
                // Simple client-side validation; you can replace with more UX later.
                return;
            }

            TalkView talk = Talks.First(t => t.Id == CurrentTalkId);

            talk.UserRating = CurrentRating;
            talk.LastComment = CommentText;
            talk.LastRatedAt = DateTime.UtcNow;

            CloseRatingModal();
        }

        protected string GetTrackClass(string track)
        {
            if (track == "Technical")
            {
                return "survey-track-technical";
            }

            if (track == "Business")
            {
                return "survey-track-business";
            }

            if (track == "Design")
            {
                return "survey-track-design";
            }

            return string.Empty;
        }

        protected string GetAverageStarIconClass(double ratingValue, int starIndex)
        {
            if (ratingValue <= 0)
            {
                return "fa-regular fa-star text-gray-300";
            }

            if (ratingValue >= starIndex)
            {
                return "fa-solid fa-star text-yellow-400";
            }

            double diff = ratingValue - (starIndex - 1);

            if (diff > 0.25 && diff < 0.75)
            {
                // There is no official "half star" in Font Awesome free;
                // we fall back to regular star here or you can use a custom icon.
                return "fa-regular fa-star text-gray-300";
            }

            return "fa-regular fa-star text-gray-300";
        }

        protected string GetAverageRatingText(TalkView talk)
        {
            if (!talk.HasAverageRating)
            {
                if (talk.UserRating > 0)
                {
                    return "(" + talk.UserRating.ToString("0.0") + ")";
                }

                return "Not rated";
            }

            return "(" + talk.AverageRating.ToString("0.0") + ")";
        }

        protected string GetInteractiveStarIconClass(int starIndex)
        {
            if (CurrentRating >= starIndex)
            {
                return "fa-solid fa-star text-yellow-400";
            }

            return "fa-regular fa-star text-gray-300";
        }

        private void ResetRatingInternal()
        {
            CurrentRating = 0;
            RatingLabel = "Click to rate";
        }

        private string GetRatingLabel(int rating)
        {
            if (rating == 1)
            {
                return "Poor";
            }

            if (rating == 2)
            {
                return "Fair";
            }

            if (rating == 3)
            {
                return "Good";
            }

            if (rating == 4)
            {
                return "Very Good";
            }

            if (rating == 5)
            {
                return "Excellent";
            }

            return "Click to rate";
        }
    }
}
