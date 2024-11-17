﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Spec.Features.Auth
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Login")]
    [NUnit.Framework.CategoryAttribute("SeedUsers")]
    public partial class LoginFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = new string[] {
                "SeedUsers"};
        
#line 1 "Login.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Auth", "Login", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login - Unauthorized")]
        [NUnit.Framework.CategoryAttribute("LoginUnauthorized")]
        public void Login_Unauthorized()
        {
            string[] tagsOfScenario = new string[] {
                    "LoginUnauthorized"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login - Unauthorized", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 5
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 6
        testRunner.When("I make a POST request to /api/login with the payload:", "{\r\n    \"email\": \"invalid@email.com\",\r\n    \"password\": \"password\"\r\n}", ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 13
        testRunner.Then("the response status code should be 401", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login - Invalid")]
        [NUnit.Framework.CategoryAttribute("LoginUnprocessableEntity")]
        [NUnit.Framework.TestCaseAttribute("null", "\"password\"", "{\"email\": [\"error.email.required\"]}", null)]
        [NUnit.Framework.TestCaseAttribute("\"\"", "\"password\"", "{\"email\": [\"error.email.required\"]}", null)]
        [NUnit.Framework.TestCaseAttribute("\"invalid-email\"", "\"password\"", "{\"email\": [\"error.email.invalid\"]}", null)]
        [NUnit.Framework.TestCaseAttribute("\"valid@email.com\"", "null", "{\"password\": [\"error.password.required\"]}", null)]
        [NUnit.Framework.TestCaseAttribute("\"valid@email.com\"", "\"\"", "{\"password\": [\"error.password.required\"]}", null)]
        [NUnit.Framework.TestCaseAttribute("\"valid@email.com\"", "\"short\"", "{\"password\": [\"error.password.tooShort\"]}", null)]
        [NUnit.Framework.TestCaseAttribute("\"valid@email.com\"", "\"longlonglonglonglonglonglonglonglonglonglonglonglonglonglonglong\"", "{\"password\": [\"error.password.tooLong\"]}", null)]
        public void Login_Invalid(string email, string password, string validationErrors, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "LoginUnprocessableEntity"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Email", email);
            argumentsOfScenario.Add("Password", password);
            argumentsOfScenario.Add("ValidationErrors", validationErrors);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login - Invalid", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 16
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 17
        testRunner.When("I make a POST request to /api/login with the payload:", string.Format("{{\r\n    \"email\": {0},\r\n    \"password\": {1}\r\n}}", email, password), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 24
        testRunner.Then("the response status code should be 422", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 25
        testRunner.And("the response should contain", string.Format("{{\r\n    \"errors\" : {0}\r\n}}", validationErrors), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login - Ok")]
        [NUnit.Framework.CategoryAttribute("LoginOk")]
        public void Login_Ok()
        {
            string[] tagsOfScenario = new string[] {
                    "LoginOk"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login - Ok", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 42
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 43
        testRunner.When("I make a POST request to /api/login with the payload:", "{\r\n    \"email\": \"test.admin@email.com\",\r\n    \"password\": \"password\"\r\n}", ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 50
        testRunner.Then("the response status code should be 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 51
        testRunner.And("the response should contain", "{\r\n    \"user\": {\r\n        \"id\": 13786,\r\n        \"email\": \"test.admin@email.com\",\r" +
                        "\n        \"role\": \"Admin\"\r\n    }\r\n}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
