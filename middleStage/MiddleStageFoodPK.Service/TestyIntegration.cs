using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleStageFoodPK.Service;

public class TestyIntegration(IHttpClientFactory factory) {
    private readonly IHttpClientFactory factory = factory;

    public async Task<string> GetListViewTest()
    {
        var client = factory.CreateClient("salesforce");
        var response = await client.GetAsync("/services/data/v59.0/sobjects/Account/listviews/");
        return await response.Content.ReadAsStringAsync();
    }

}
