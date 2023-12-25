using TechTalk.SpecFlow;
using CookingAPIClient;
using System.Xml.Linq;

namespace Cooking.Specs.StepDefinitions
{
    [Binding]
    public class CookwareStepDefinitions
    {
        private CookwareForCreationDto cookwareToCreate;
        private CookwareForUpdateDto cookwareToUpdate;
        private List<Operation> operations;
        private CookwareDto createdCookware;
        private CookwareDto recievedCookware;
        private int errorStatusCode;
        private string errorMessage;
        private bool hasUpdated;
        private bool hasPartiallyUpdated;
        private bool hasDeleted;
        private ScenarioContext scenarioContext;
        private swaggerClient client;

        public CookwareStepDefinitions(CookwareForCreationDto cookwareToCreate, CookwareForUpdateDto cookwareToUpdate, ScenarioContext scenarioContext)
        {
            this.cookwareToCreate = cookwareToCreate;
            this.cookwareToUpdate = cookwareToUpdate;
            this.scenarioContext = scenarioContext;
            var httpClient = new HttpClient();
            this.client = new swaggerClient(httpClient);
        }

        [Given(@"Cookware with name ""(.*)""")]
        public void GivenCookwareWithName(string name)
        {
            this.cookwareToCreate.Name = name;
        }

        [Given(@"Cookware without name")]
        public void GivenCookwareWithoutName()
        {
            this.cookwareToCreate.Name = "";
        }

        [Given(@"cookware with color ""(.*)""")]
        public void GivenCookwareWithColor(Color color)
        {
            this.cookwareToCreate.Color = color;
        }

        [Given(@"This cookware's updated name is ""(.*)""")]
        public void GivenThisCookwaresUpdatedNameIs(string name)
        {
            this.cookwareToUpdate.Name = name;
        }

        [Given(@"This cookware's updated name is missing")]
        public void GivenThisCookwaresUpdatedNameIsMissing()
        {
            this.cookwareToUpdate.Name = "";
        }

        [Given(@"this cookware's updated color is ""(.*)""")]
        public void GivenThisCookwaresUpdatedColorIs(Color color)
        {
            this.cookwareToUpdate.Color = color;
        }

        [Given(@"This cookware's new name is ""(.*)""")]
        public void GivenThisCookwaresNewNameIs(string name)
        {
            Operation operation = new Operation();
            operation.Op = "replace";
            operation.Path = "/name";
            operation.Value = name;

            operations = new List<Operation>();
            operations.Add(operation);
        }

        [Given(@"This cookware's new name is missing")]
        public void GivenThisCookwaresNewNameIsMissing()
        {
            Operation operation = new Operation();
            operation.Op = "replace";
            operation.Path = "/name";
            operation.Value = "";

            operations = new List<Operation>();
            operations.Add(operation);
        }

        [When(@"Send request to create cookware")]
        public async Task WhenSendRequestToCreateCookwareAsync()
        {
            try
            {
                this.createdCookware = await client.CreateCookWareAsync(this.cookwareToCreate);
            }
            catch(ApiException e)
            {
                this.errorStatusCode = e.StatusCode;
                this.errorMessage = e.Response;
            }
        }

        [When(@"Send request to delete this cookware")]
        [When(@"Send request to delete this cookware again")]
        public async Task WhenSendRequestToDeleteThisCookwareAsync()
        {
            try
            {
                await this.client.DeleteCookwareAsync(this.createdCookware.Id);
                this.hasDeleted = true;
            }
            catch (ApiException e)
            {
                this.hasDeleted = false;
                this.errorStatusCode = e.StatusCode;
            }
        }

        [When(@"Send request to update this cookware")]
        public async Task WhenSendRequestToUpdateThisCookwareAsync()
        {
            try
            {
                await this.client.UpdateCookwareAsync(this.createdCookware.Id, this.cookwareToUpdate);
                this.hasUpdated = true;
            }
            catch (ApiException e)
            {
                this.hasUpdated = false;
                this.errorStatusCode = e.StatusCode;
                this.errorMessage = e.Response;
            }
        }

        [When(@"Send request to partially update this cookware")]
        public async Task WhenSendRequestToPartiallyUpdateThisCookwareAsync()
        {
            try
            {
                await this.client.PartiallyUpdateCookwareAsync(createdCookware.Id, operations);
                hasPartiallyUpdated = true;
            }
            catch(ApiException e)
            {
                hasPartiallyUpdated = false;
                this.errorStatusCode = e.StatusCode;
                this.errorMessage = e.Response;
            }
        }

        [When(@"Send request to get this cookware")]
        public async Task WhenSendRequestToGetThisCookwareAsync()
        {
            try
            {
                this.recievedCookware = await this.client.GetCookwareAsync(this.createdCookware.Id);
            }
            catch(ApiException e)
            {
                this.errorStatusCode = e.StatusCode;
            }
        }

        [Then(@"Validate the created cookware's name is ""(.*)""")]
        public void ThenValidateTheCreatedCookwaresNameIs(string name)
        {
            Assert.AreEqual(this.createdCookware.Name, name);
        }

        [Then(@"Validate the created cookware's color is ""(.*)""")]
        public void ThenValidateTheCreatedCookwaresColorIs(Color color)
        {
            Assert.AreEqual(this.createdCookware.Color, color);
        }

        [Then(@"Validate cookware is deleted")]
        public void ThenValidateCookwareIsDeleted()
        {
            Assert.AreEqual(this.hasDeleted, true);
        }

        [Then(@"Validate this cookware is updated")]
        public void ThenValidateThisCookwareIsUpdated()
        {
            Assert.AreEqual(this.hasUpdated, true);
        }

        [Then(@"Validate this cookware is partially updated")]
        public void ThenValidateThisCookwareIsPartiallyUpdated()
        {
            Assert.AreEqual(this.hasPartiallyUpdated, true);
        }

        [Then(@"Validate the recieved cookware's name is ""(.*)""")]
        public void ThenValidateTheRecievedCookwaresNameIs(string name)
        {
            Assert.AreEqual(this.recievedCookware.Name, name);
        }

        [Then(@"Validate the receieved cookware's color is ""(.*)""")]
        public void ThenValidateTheReceievedCookwaresColorIs(Color color)
        {
            Assert.AreEqual(this.recievedCookware.Color, color);
        }

        [Then(@"Expect Error with StatusCode ""(.*)"" and message ""(.*)""")]
        public void ThenExpectErrorWithErrorCodeAndMessage(int statusCode, string message)
        {
            Assert.AreEqual(this.errorStatusCode, statusCode);
            Assert.AreEqual(this.errorMessage.Contains(message), true);
        }

        [Then(@"Expect Error with StatusCode ""(.*)""")]
        public void ThenExpectErrorWithStatusCode(int statusCode)
        {
            Assert.AreEqual(this.errorStatusCode, statusCode);
        }
    }
}