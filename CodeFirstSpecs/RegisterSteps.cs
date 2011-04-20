using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using MVCStore.AcceptanceTest;
using WatiN.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeFirstAltairis.Models;

namespace CodeFirstSpecs {
    [Binding]
    public class RegisterSteps : Steps {

        [BeforeScenario]
        public void ScenarioSetup() {
            ScenarioContext.Current["stepcount"] = 0;
            UserRepository userRep = new UserRepository();
            userRep.Delete("jack");
            userRep.Save();
        }

        [Given(@"I am on the Login Page")]
        public void GivenIAmOnTheLoginPage() {
            WebBrowser.Current.GoTo("http://localhost:57802/Account/LogOn");
            WebBrowser.Current.BringToFront();
        }

        [When(@"I click Register")]
        public void WhenIClickRegister() {
            WebBrowser.Current.Link(Find.ByText("Register")).Click();
        }

        [Then(@"I am on the Registration Page")]
        public void ThenIAmOnTheRegistrationPage() {
            Assert.IsTrue(WebBrowser.Current.ElementWithTag("h2",Find.ByText("Create a New Account")).Exists);
        }

        [Given(@"I am on the Registration Page")]
        public void GivenIAmOnTheRegistrationPage() {
            Given("I am on the Login Page");
            When("I click Register");
            incStepCount();
        }

        [When(@"I fill in the Form as follows")]
        public void GivenIFillInTheFormAsFollows(TechTalk.SpecFlow.Table table) {
            foreach (var row in table.Rows) {
                var labelText = row["Label"];
                var value = row["Value"];
                WebBrowser.Current.TextFields.First(Find.ByLabelText(labelText)).TypeText(value);
            }
        }

        [Then(@"I am on the Home Page Logged In")]
        public void ThenIAmOnTheHomePageLoggedIn() {
            Assert.IsTrue(WebBrowser.Current.Link(Find.ByText("Log Off")).Exists);
        }

        [When(@"I click the Register button")]
        public void WhenIClickTheRegisterButton() {
            WebBrowser.Current.Button(Find.ByValue("Register")).Click();
        }

        private void incStepCount() {
            int i = ((int)ScenarioContext.Current["stepcount"]);
            ScenarioContext.Current["stepcount"] = ++i;
        }
    }
}
