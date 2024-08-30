using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineSuggestionController : ControllerBase
    {
        private const string AmazonUrl = "https://www.amazon.eg";

        // GET: api/OnlineSuggestion/{productName}
        [HttpGet("{productName}")]
        public async Task<ActionResult<IEnumerable<ProductDetail>>> GetSuggestions(string productName)
        {
            try
            {
                var suggestions = await GetAmazonSuggestions(productName);
                return Ok(suggestions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task<List<ProductDetail>> GetAmazonSuggestions(string productName)
        {
            var suggestions = new List<ProductDetail>();

            try
            {
                // Construct the Amazon search URL for the specified product name
                string searchUrl = $"{AmazonUrl}/s?k={Uri.EscapeDataString(productName)}";

                // Setup HttpClient and HtmlDocument
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(searchUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException($"Response status code does not indicate success: {response.StatusCode} ({response.ReasonPhrase}).");
                    }

                    var html = await response.Content.ReadAsStringAsync();
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);

                    // XPath to find product details
                    var nodes = htmlDocument.DocumentNode.SelectNodes("//div[@data-asin]");

                    if (nodes != null)
                    {
                        foreach (var node in nodes)
                        {
                            // Extract product details
                            var titleNode = node.SelectSingleNode(".//h2//a");
                            var link = titleNode?.GetAttributeValue("href", string.Empty);
                            if (!string.IsNullOrEmpty(link) && !link.StartsWith("http"))
                            {
                                link = AmazonUrl + link;
                            }

                            var title = titleNode?.InnerText.Trim();

                            var priceNode = node.SelectSingleNode(".//span[@class='a-price']/span[@class='a-offscreen']");
                            var price = priceNode?.InnerText.Trim();

                            var imageNode = node.SelectSingleNode(".//img[@src]");
                            var image = imageNode?.GetAttributeValue("src", string.Empty);

                            suggestions.Add(new ProductDetail
                            {
                                Title = title,
                                Link = link,
                                Price = price,
                                ImageUrl = image
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Amazon suggestions: {ex.Message}");
            }

            return suggestions;
        }
    }

    public class ProductDetail
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
