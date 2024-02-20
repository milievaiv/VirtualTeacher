using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/404")]
    public IActionResult NotFoundError()
    {
        return View("Error404");
    }

    [Route("Error/403")]
    public IActionResult Forbidden()
    {
        return View("Forbidden");
    }

    [Route("Error/500")]
    public IActionResult Error()
    {
        return View("Error");
    }

    //[Route("Error/{statusCode}")]
    //public IActionResult HandleError(int statusCode)
    //{
    //    switch (statusCode)
    //    {
    //        case 404:
    //            return View("Error404");
    //        case 403:
    //            return View("Forbidden");
    //        case 500:
    //            return View("InternalServerError");
    //        default:
    //            return View("Error");
    //    }
    //}
}
