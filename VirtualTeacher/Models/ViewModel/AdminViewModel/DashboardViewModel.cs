namespace VirtualTeacher.Models.ViewModel.AdminViewModel
{
    public class DashboardViewModel
    {
        public IList<Course> RecentCourses { get; set; }
        public IList<Log> RecentLogs { get; set; }
        public int TotalAdmins { get; set; }
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public int TotalEnrollments { get; set; }
        public int TotalTeachers { get; set; }
    }
}
