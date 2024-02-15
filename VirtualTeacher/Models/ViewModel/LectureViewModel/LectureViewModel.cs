namespace VirtualTeacher.Models.ViewModel.LectureViewModel
{
    public class LectureViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }

        // Assignment Details
        public int AssignmentId { get; set; }
        public string AssignmentContent { get; set; } // Assuming you want to display the content of the assignment
                                                      // Add more properties if needed, such as submission details
    }

}
