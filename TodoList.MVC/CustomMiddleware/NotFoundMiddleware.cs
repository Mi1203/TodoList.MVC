namespace TodoList.MVC.CustomMiddleware
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Call next middleware in pipeline
            await _next(context);

            //Check if status 404
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                // Redirect to 
                context.Request.Path = "/NotFound";
                context.Response.StatusCode = StatusCodes.Status200OK; //change status
                await _next(context); // call next middleware again
            }
        }
    }

}
