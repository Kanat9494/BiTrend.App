namespace BiTrend.Models;

internal class ResponseHttpMessage
{
    public int ResponseId { get; set; }
    public short StatusCode { get; set; }
    public string Message { get; set; }
}
