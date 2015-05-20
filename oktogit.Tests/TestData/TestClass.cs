using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oktogit.Tests
{
    class TestClass
    {//This should be found.

        string shouldNotBeFound = "Http://testheredsalao";

        string shouldBeFound = "dsa";// This should be found.

        // This should be found.

        /// This should not bee found.
        ///  // This should not be found.
        ///  /* This should not be found.
        ///  * This should not be found.
        ///  */ This should not be found.
        
        /*This should not be found.*/

        /* This should not be found.
         * This should not be found.
        This should not be found.
        This should not be found. */

        //TODO: This should be found.
        //TODO This should be found.
        //todo This should be found.

        // IT should be 7 found lines total (including this one) where 3 are TODO comments
    }
}
