using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Consts
{
    public static class ResultExtension
    {
        public static ObjectResult ToProblem(this Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Can not convert from success result to problem");

            var problem = (ProblemHttpResult)Results.Problem(statusCode: result.Error.StatusCode);
            var problemDetail = problem.ProblemDetails;

            problemDetail.Extensions = new Dictionary<string, object?>
            {

                {
                    "errors",new []{
                     result.Error.Code
                    ,result.Error.Description
                    }
                }
            };

            return new ObjectResult(problemDetail);
        }
    }
}
