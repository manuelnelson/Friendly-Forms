namespace BusinessLogic.Models
{
    public class FormMenuItem
    {
        public string itemClass { get; set; }
        public string path { get; set; }
        public string pathIdentifier { get; set; }
        public string iconClass { get; set; }
        public string text { get; set; }
        public string formName { get; set; }
        public long userId { get; set; }
        public bool hasChildren { get; set; }
        public bool isComplete { get; set; }
        //{
        //    get
        //    {
        //        if (!hasChildren)
        //        {
        //            var viewModel = FormService.GetByUserId(userId);
        //            if (viewModel != null)
        //                return true;
        //            return false;
        //        }
        //        return false;
        //    }
        //}
        //public IFormService<TRepository, TEntity> FormService { get { return TService; } set; } 
    }
}
