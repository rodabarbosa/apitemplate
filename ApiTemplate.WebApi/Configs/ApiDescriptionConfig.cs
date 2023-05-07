namespace ApiTemplate.WebApi.Configs;

/// <summary>
/// Api Drscription
/// </summary>
public sealed class ApiDescriptionConfig
{
    /// <summary>
    /// Api Title
    /// </summary>
    /// <remarks>
    /// Version will be added automatically
    /// </remarks>
    public string? Title { get; set; }

    /// <summary>
    /// Api description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Address to term of service
    /// </summary>
    public string? TermOfService { get; set; }

    /// <summary>
    /// Api contact address
    /// </summary>
    public string? ContactEmail { get; set; }

    /// <summary>
    /// Api contact name
    /// </summary>
    public string? ContactName { get; set; }

    /// <summary>
    /// Api contact site
    /// </summary>
    public string? ContactSite { get; set; }
}
