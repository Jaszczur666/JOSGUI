using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JOSTest
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void Fit_Works()
        {
            string s1, s2; 
            JOS.Solver tests = new JOS.Solver();
            JOS.Multiplet testm = new JOS.Multiplet();
            testm.u2 = new System.Collections.Generic.List<double>();
            testm.u4 = new System.Collections.Generic.List<double>();
            testm.u6 = new System.Collections.Generic.List<double>();
            testm.fexp = new System.Collections.Generic.List<double>();
            testm.lambda0 = new System.Collections.Generic.List<double>();
            testm.n = 1.95;
            testm.TwoJPlusOne = 13;
            testm.u2.Add(0.001); testm.u2.Add(0.002); testm.u2.Add(0.004); testm.u2.Add(0.301);
            testm.u4.Add(0.001); testm.u4.Add(0.343); testm.u4.Add(0.054); testm.u4.Add(0.101);
            testm.u6.Add(0.001); testm.u6.Add(0.212); testm.u6.Add(0.404); testm.u6.Add(0.501);
            testm.fexp.Add(1e-6); testm.fexp.Add(3.56e-6); testm.fexp.Add(2.34e-6); testm.fexp.Add(0.34e-6);
            testm.lambda0.Add(345e-7); testm.lambda0.Add(645e-7); testm.lambda0.Add(349e-7); testm.lambda0.Add(475e-7);
            tests.FitLM(testm, out s1,out s2);
            double testchi2= tests.Chi2(testm);
            Assert.IsTrue((testchi2 < 1e-7),"Słaba zbieżność");
            
        }
        [TestMethod]
        public void Omegas_Correct()
        {

        }
    }
}
