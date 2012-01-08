using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
﻿using TechTalk.SpecFlow;
using WatiN.Core;

namespace MVCStore.AcceptanceTest {
[Binding]
public static class WebBrowser {
        public static IE Current {
            get {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                    ScenarioContext.Current["browser"] = new IE();
                return (IE)ScenarioContext.Current["browser"];
            }
        }

        [AfterScenario]
        public static void Close() {
            if (ScenarioContext.Current.ContainsKey("browser"))
                WebBrowser.Current.Close();
        }
    }
}
