﻿namespace MyWebServer.HTTP.RequestResponse.Response
{
    public enum HttpStatus
    {
        Ok = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,

        MovedPermanently = 301,
        Found = 302,
        SeeOther = 303,
        NotModified = 304,
        TemporaryRedirect = 307,
        PermanentRedirect = 308,

        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        Conflict = 409,
        Gone = 410,
        PayloadTooLarge = 413,
        UnsupportedMediaType = 415,
        TooManyRequests = 429,

        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504
    }
}
