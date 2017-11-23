using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmN_2010
{
    class rotationModel2
    {
        public rotationModel2()
        {
        }
        public bool CalcRotation(double[] A, double[] B, ref double[][] ObjectCoeff, ref double[][] arrRotationList)
        {
            //for (int i = 0; i < A.Count(); i++)
            //{
            //    A[i] = Math.Round(A[i],2);

            //}
            //double totalA=A.Sum();
            //double totalB = B.Sum();
            //double diff = totalB - totalA;
            //if (diff > (0.01 * B.Count()) || diff < (-0.01 * B.Count()))
            //{
            //    message.Instance.addWarnings("Der er noget galt med afstemning i gødningsberegning", "RotationModel2: CalcRotation: Difference mll. A og B for stor", 2);
            //}
            double Delta=0;
            //A[A.Count() - 1] = A[A.Count() - 1] + Math.Round(diff,2);
            int k=0, l=0;
            double[][] BasicSolution = arrRotationList;
            int x =A.Count()+B.Count();
            int y = A.Count() + B.Count()-1;
            int[] path = new int[x];
            double[][] TestTree = new double[x][]; ;
            double[][] BasisTree = new double[x][];
            bool returnValue = false;
            int PathFound;
            bool Optimal = false;
            double[] DualPrice = new double[x];
    
            int DimA=A.Count();
            int DimB = B.Count();
            double[][] ReducedCosts = new double[A.Count()][];
            for (int i = 0; i < ReducedCosts.Count(); i++)
            {
                ReducedCosts[i] = new double[B.Count()];
            }
            int p = 0, q = 0; ;
            InitBasicSolution(ref BasicSolution, ref BasisTree, ref A, ref B, ref y, B.Count());
            double Z = 0;
            double Z_old=0;
            int ConstObj=0;
            bool NoSolution=true;
            while (!Optimal)
            {
                Z = CalcObjFctValue(ref ObjectCoeff, ref BasicSolution);

                for (int i = 0; i < BasisTree.Count(); i++)
                {
                    TestTree[i] = new double[BasisTree[i].Count()];
                    for (int j = 0; j < BasisTree[j].Count(); j++)
                    {
                        TestTree[i][j] = BasisTree[i][j];
                    }
                }
                CalcDualPrice(ref TestTree, ref DualPrice, ref ObjectCoeff, ref DimA, ref DimB);
                CalcReducedCosts(ref ReducedCosts, ref DualPrice, ref ObjectCoeff, ref DimA, ref DimB);
                FindMinArr(ref p, ref q, ref ReducedCosts, ref Optimal);
                if (!Optimal) 
                 {

                     for (int i = 0; i < TestTree.Count(); i++)
                         for (int j = 0; j < TestTree[j].Count(); j++)
                             TestTree[i][j] = BasisTree[i][j];
                     int nul = 0;
                     int minusone = -1;
                     InitPath(ref path, ref nul, ref minusone);
                     PathFound = 0;
                     FindPath(ref TestTree, ref p, ref q, ref path, ref nul, ref PathFound);
                     
                     FindOutVar(ref Delta,ref k,ref l,ref path, ref BasicSolution,ref DimA,ref DimB);

                     UpdateBasicSolution(ref path,ref Delta,ref BasicSolution,ref DimA,ref DimB);
                     UpdateBasisTree(ref p,ref q,ref k,ref l,ref BasisTree);

                 }
                if(Z==Z_old) 
                    ConstObj = ConstObj+1;
                else 
                 ConstObj = 0;
                Z_old = Z;
               if(ConstObj==100)
                   break;
            }

            NoSolution = CalcNoSolution(ref BasicSolution, ref ObjectCoeff);
            arrRotationList = BasicSolution;
            if (Optimal && !(NoSolution))
                returnValue = true;

            return returnValue;

        }
        double CalcObjFctValue(ref double[][] ObjectCoeff, ref double[][] BasicSolution)
        {
            double value=0;
 

            for(int i=0;i<BasicSolution.Count();i++)
                for (int j = 0; j < BasicSolution[i].Count(); j++)
                {
                    double part1 = BasicSolution[i][j];
                    double part2 = ObjectCoeff[i][j];
                    value = value + Math.Round(BasicSolution[i][j],2) * ObjectCoeff[i][j];
                }

            return value;
        }
        void InitBasicSolution(ref double[][] BS, ref double[][] BT, ref double[] A, ref double[] B, ref int BTy, int BSy)
        {


            double[] CA1 = B;
            double[] CA2 = A;
            int NBcol=0;
            int rows = CA2.Count()-1;
            int cols = CA1.Count()-1;

          //  int NBcol=0;
            for(int i=0;i<BS.Count();i++)
            {
                BS[i] = new double[BSy];
                for (int j = 0; j < BSy; j++)
                    BS[i][j] = 0;
            }

            for(int i=0;i<BT.Count();i++)
            {
                BT[i] = new double[BTy];
                for (int j = 0; j < BTy; j++)
                    BT[i][j] = 0;
            }
            int r=0;
            int c=cols;

            SolveAll(ref r,ref c,ref BS,ref BT,ref CA1,ref CA2,ref NBcol);
        }

        void SolveAll(ref int i, ref int j, ref double[][] A, ref double[][] BT, ref double[] CA1, ref  double[] CA2, ref int NBcol)
        {
            //PrintArrayInTable1Rot CA1
            //PrintArrayInTable1Rot CA2
            
            while( i <= (CA2.Count()-1) && j >= 0)
            {

                Solve(ref i,ref j,ref A,ref BT,ref CA1,ref CA2,ref NBcol);

                NextCell(ref i,ref j,ref A,ref CA1,ref CA2,ref NBcol);

                SolveAll(ref i,ref j,ref A,ref BT,ref CA1,ref CA2,ref NBcol);

            }


        }
        void Solve(ref int i, ref int j, ref double[][] A, ref double[][] BT, ref double[] CA1, ref double[] CA2, ref int NBcol)
        {
            double y ;
		    double x1, x2;
		    x1 = CA1[j];
		    x2 = CA2[i];
            y = Math.Min(x1,x2);
            A[i][j] =y;
            BT[i][NBcol] = 1;
            CA2.Count();
            int k = CA2.Count();
            BT[j+CA2.Count()][NBcol]=-1;
            CA1[j] = CA1[j] - y;
            CA2[i] = CA2[i] - y;
        }
        void NextCell(ref int i, ref int j, ref double[][] A, ref double[] CA1, ref double[] CA2, ref int NBcol)
        {

            if(CA1[j] < 0.0001 && CA2[i] >= 0.0001)
                j = j - 1;
            if(CA2[i] < 0.0001 && CA1[j] >= 0.0001)
                i = i + 1;
            if(CA1[j] < 0.0001 && CA2[i] < 0.0001)
            {
              if(i<( CA2.Count()-1))
                 i = i + 1;
              else 
                 j = j - 1;
            }
            NBcol = NBcol + 1;
        }
        void CalcDualPrice(ref double[][] TestTree, ref double[] DualPrice, ref double[][] ObjectCoeff, ref int DimA, ref int DimB)
        {
            int startknude;

            startknude = TestTree.Count()-1 ;

            DualPrice[startknude] = 0;
            DepthFirst(ref TestTree, ref startknude, ref DualPrice, ref ObjectCoeff, ref DimA, ref DimB);
        }
        void DepthFirst(ref double[][] tree, ref int n1, ref double[] DualPrice, ref double[][] ObjectCoeff, ref int DimA, ref int DimB)
        {
            int n2 = FindNextChild(ref tree, ref n1);

            while( n2>-1)
            {
                DoSomething(ref tree, ref n1, ref n2, ref DualPrice, ref ObjectCoeff, ref DimA, ref DimB);
                SetVisited(ref tree, ref n1, ref n2);
                DepthFirst(ref tree, ref n2, ref DualPrice, ref ObjectCoeff, ref DimA, ref DimB);
                n2 = FindNextChild(ref tree, ref n1);
            }
        }

        int FindNextChild(ref double[][] tree, ref int n)
        {
           int returnValue = -1;
            for (int j = 0; j < tree[n].Count(); j++)
            {

                if (tree[n][j] == 1 || tree[n][j] == -1)
                {
                    for (int i = 0; i < tree.Count(); i++)
                        if (!(i == n))
                            if (tree[i][j] == 1 || tree[i][j] == -1)
                            {
                                returnValue = i;
                                return returnValue;
                            }

                }
            }
              return returnValue;
        }
        void DoSomething(ref double[][] tree, ref int n1, ref int n2, ref double[] DualPrice, ref double[][] ObjectCoeff, ref int DimA, ref int DimB)
        {

            if (n1 < n2)
                DualPrice[n2] = DualPrice[n1] - ObjectCoeff[n1][n2 - DimA];
            else
                DualPrice[n2] = ObjectCoeff[n2][n1 - DimA] + DualPrice[n1];

        }

        void SetVisited(ref double[][] tree, ref int n1, ref int n2)
      {

        for(int j=0;j<tree[j].Count();j++)
          if(!(tree[n1][j]==0) && !(tree[n2][j]==0))
          {
            tree[n1][j]=2*tree[n1][j];
            tree[n2][j]=2*tree[n2][j];

          }

      }
        void CalcReducedCosts(ref double[][] ReducedCosts, ref double[] DualPrice, ref double[][] ObjectCoeff, ref int DimA, ref int DimB)
      {
        for(int i=0;i<ReducedCosts.Count();i++)
          for(int j=0;j<ReducedCosts[i].Count();j++)
          {
            ReducedCosts[i][j]=DualPrice[i] -DualPrice[j+DimA] - ObjectCoeff[i][j];

          }

        
       }

      void FindMinArr(ref int p, ref int q, ref double[][] ReducedCosts, ref bool Optimal)
    {   
        p=-1;
        q=-1;
        double min=0;

        for(int i=0;i<ReducedCosts.Count();i++)
        {

          for(int j=0;j<ReducedCosts[i].Count();j++)
          {
              if(ReducedCosts[i][j]<min)
              {
                  min=ReducedCosts[i][j];
                  p=i;
                  q=j;

              }
          }
        }
        if (min == 0)
            Optimal = true;
        else
            q = q + ReducedCosts.Count() ;

        }
    void InitPath(ref int[] s, ref int d, ref int v)
        {


            for (int i = d; i<s.Count(); i++)
                s[i] = v;
  

        }
    void FindPath(ref double[][] tree, ref int n1, ref int n2, ref int[] s, ref int d, ref int PathFound)
        {


            WritePath(ref n1, ref s, ref d);
            int n = FindNextChild(ref tree, ref n1);       
            while(PathFound == 0)
            {
                d = DepthInTree(ref n, ref d);
              if(n==-1)
                  break;
              SetVisited(ref tree, ref n1, ref n);
              if(n==n2)
              {
                  WritePath(ref n, ref s, ref d);
                PathFound = 1;
                break;
              }
              FindPath(ref tree, ref n, ref n2, ref s, ref d, ref PathFound);
              n = FindNextChild(ref tree, ref n1);
            }
        }
        void WritePath(ref int n, ref int[] s, ref int d)
        {
            
            s[d] = n;
            int minusOne = -1;
            int dPlusOne =d+1;
            InitPath(ref s, ref dPlusOne, ref minusOne);
        } 
        int DepthInTree(ref int n, ref int d)
        {
            if(n>-1) 
              d = d + 1;
            else
              d = d - 1;
            return d;
        }
        static int t = 0;
        void FindOutVar(ref double Delta, ref int k, ref int l, ref int[] path, ref double[][] BasicSolution, ref int DimA, ref int DimB)
     {
         t++;
        k=path[0];
        l=path[1];

        Delta = BasicSolution[path[0]][path[1]-DimA];


        for(int i=1;i<=(path.Count()-1)/2;i++)
         {
          if(path[2*i]==-1)
              break;

          int problem = path[2 * i + 1] - DimA;

          if (Delta > BasicSolution[path[2 * i]][problem])
          {

	          Delta = BasicSolution[path[2*i]][path[2*i+1]-DimA];
              k=path[2*i];
              l=path[2*i+1];
          }
         }
    }
        void UpdateBasicSolution(ref int[] path, ref double Delta, ref double[][] BasicSolution, ref int DimA, ref int DimB)
    {

        for(int i=0;i<=(path.Count()-1)/2;i++)
         {
          if(path[2*i]==-1)
              break;
          BasicSolution[path[2*i]][path[2*i+1]-DimA]=BasicSolution[path[2*i]][path[2*i+1]-DimA]-Delta;

         }
        int it;
        for (it = 1; it <= (path.Count()-1) / 2; it++)
        {
          if(path[2*it]==-1)
              break;
          BasicSolution[path[2 * it]][path[2 * it - 1] - DimA] = BasicSolution[path[2 * it]][path[2 * it - 1] - DimA] + Delta;

         }

        BasicSolution[path[0]][path[2*it-1]-DimA]=Delta;


    }
        void UpdateBasisTree(ref int p, ref int q, ref int k, ref int l, ref double[][] BasisTree)
        {

               for(int j=0;j< BasisTree[k].Count();j++)
               {
                 if(BasisTree[k][j]==1 && BasisTree[l][j]==-1)
                 {
                   BasisTree[k][j]=0;  
                   BasisTree[l][j]=0;
                   BasisTree[p][j]=1;
                   BasisTree[q][j]=-1;     
                 }
               }
               
        }
    bool CalcNoSolution(ref double[][] BasicSolution, ref double[][] ObjectCoeff)
        {
            bool value=false;
            for(int i=0;i<BasicSolution.Count();i++)
                for(int j=0;j<BasicSolution[i].Count();j++)
                    if(Math.Round(BasicSolution[i][j],2)>0 && ObjectCoeff[i][j]<-1000) 
	                    value=true;

            return value;
        }


    }
     
}