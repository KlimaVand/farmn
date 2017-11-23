PROC IMPORT OUT= WORK.original 
            DATAFILE= "C:\D\Job2459\Data\Udvalgte senarier.xls" 
            DBMS=EXCEL2000 REPLACE;
     RANGE="Oprindelig$"; 
     GETNAMES=YES;
RUN;
PROC IMPORT OUT= WORK.alternativ 
            DATAFILE= "C:\D\Job2459\Data\Udvalgte senarier.xls" 
            DBMS=EXCEL2000 REPLACE;
     RANGE="Alternativ$"; 
     GETNAMES=YES;
RUN;
data a;set original(in=o) alternativ;
if o then stratn='O'||put(_N_,z2.);else stratn='a'||put(_N_-16,z2.);
niveau=Genn;foraar=forar+fix;udbind=udbn;
if jordtype='s' then do;efters=efterar;efterl=0;end;else do;efters=0;efterl=efterar;end;
fjernet=nfj_norm;
array x{*} a1-a6 f1-f4;do i=1 to dim(x);x{i}=0;end;drop i;x{afgr}=100;x{6+forfr_grp}=100;
A0=per;Af=per_for;h=humus;l=ler;ar=aar;
keep stratn niveau--ar;
proc print;run;
proc transpose data=a out=strat;id stratn;var niveau--ar;run;
proc print;run;
data est;set her.est62;
proc print;run;
data cov;set her.var62;drop Row;
proc print;run;
%macro regn;
%do i=1 %to 16;
%if &i<10 %then %let ii=0&i; %else %let ii=&i;
title "Beregning usikkerhed for senarie nummer &ii";
Proc Iml;* Version med 2 og kun to variable;
USE strat;
READ ALL var{O&ii a&ii} into stratx [Rowname=_name_];strat=stratx%str(`);
USE est;
READ ALL var{estimate} INTO est [rowname=_name_];
READ ALL var{_name_} into parmname;
USE cov;
READ ALL var _NUM_ INTO cov [rowname=effect];
nstrat=ncol(stratx);jac=j(24,nstrat);yHat=j(nstrat,1);
do stratnr=1 to nstrat;
  T=est[4]*strat[stratnr,1]+est[5]*strat[stratnr,2]+est[6]*strat[stratnr,3]+est[7]*strat[stratnr,4]+est[8]*strat[stratnr,5]
   -est[9]*strat[stratnr,6]
   +est[11]*strat[stratnr,7]+est[12]*strat[stratnr,8]+est[13]*strat[stratnr,9]+est[14]*strat[stratnr,10]+est[15]*strat[stratnr,11]+est[16]*strat[stratnr,12]
   +est[16]*strat[stratnr,12]+est[17]*strat[stratnr,13]+est[18]*strat[stratnr,14]+est[19]*strat[stratnr,15]+est[20]*strat[stratnr,16];
  V=T;
  if V<=0 then do; U=est[1]+est[2]/(strat[stratnr,21]-est[3])+est[10]*V;V=0.001;end;else U=est[1]+est[2]/(2001-est[3]);
  if U<=0 then U=0;
  M=(1-exp(-est[21]*strat[stratnr,17]))*exp(-est[22]*strat[stratnr,18])
    *exp(-est[23]*strat[stratnr,19])*exp(-est[24]*strat[stratnr,20])*est[25];
  YHAT[stratnr,1]=(U+V**1.2)*M;
  jac[1,stratnr]=M;
  jac[2,stratnr]=M/(2001-est[3]);
  jac[3,stratnr]=M*est[2]*(2001-est[3])**(-2);
  jac[4,stratnr]=1.2*M*V**0.2*strat[stratnr,1];
  jac[5,stratnr]=1.2*M*V**0.2*strat[stratnr,2];
  jac[6,stratnr]=1.2*M*V**0.2*strat[stratnr,3];
  jac[7,stratnr]=1.2*M*V**0.2*strat[stratnr,4];
  jac[8,stratnr]=1.2*M*V**0.2*strat[stratnr,5];
  jac[9,stratnr]=-1.2*M*V**0.2*strat[stratnr,6];
  jac[10,stratnr]=M*min(0,T);
  jac[11,stratnr]=1.2*M*V**0.2*strat[stratnr,7];
  jac[12,stratnr]=1.2*M*V**0.2*strat[stratnr,8];
  jac[13,stratnr]=1.2*M*V**0.2*strat[stratnr,9];
  jac[14,stratnr]=1.2*M*V**0.2*strat[stratnr,10];
  jac[15,stratnr]=1.2*M*V**0.2*strat[stratnr,11];
  jac[16,stratnr]=1.2*M*V**0.2*strat[stratnr,12];
  jac[17,stratnr]=1.2*M*V**0.2*strat[stratnr,13];
  jac[18,stratnr]=1.2*M*V**0.2*strat[stratnr,14];
  jac[19,stratnr]=1.2*M*V**0.2*strat[stratnr,15];
  jac[20,stratnr]=1.2*M*V**0.2*strat[stratnr,16];
  jac[21,stratnr]=(U+V**1.2)*exp(-est[22]*strat[stratnr,18])*exp(-est[23]*strat[stratnr,19])*exp(-est[24]*strat[stratnr,20])*
     est[25]*exp(-est[21]*strat[stratnr,17])*strat[stratnr,17];
  jac[22,stratnr]=-(U+V**1.2)*M*strat[stratnr,18];
  jac[23,stratnr]=-(U+V**1.2)*M*strat[stratnr,19];
  jac[24,stratnr]=-(U+V**1.2)*M*strat[stratnr,20];
  print T U V M;
  end;
Varcov=jac`*cov*jac;c={1 -1};VD=c*VarCov*c`;
r=VarCov[1,2]/sqrt(VarCov[1,1]*VarCov[2,2]);VarDif=diag(varcov)+vd*(j(2,2)-i(2));
*Næste linie:Beregning primo aug 2004 med/uden lack of fit; 
*VarPred=VarDif+diag(Yhat)**2*(0.0953/1+0.0112/1);*VarPred=VarDif+diag(Yhat)**2*(0.0953/1+0.0112/1+0.181/1);
*Næste linie: med SigmaL2 added som beregningern dec 2004;
*VarPred=VarDif+diag(Yhat)**2*(0.0953/1+0.0112/1+0.181/1)+sum(diag(Yhat)**2)*(0.181/1)*(j(2,2)-i(2));
* Næste linie med sigmaL2 inkludert som relativ varians på den absolutte differens;
*VarPred=VarDif+diag(Yhat)**2*(0.0953/1+0.0112/1+0.181/1)+abs(Yhat[1,1]-Yhat[2,1])**2*(0.181/1)*(j(2,2)-i(2));
* Næste linie med sigmaL2 inkludert som relativ varians på den absolutte differens med korrektion for kovarians;
VarPred=VarDif+diag(Yhat)**2*(0.0953/1+0.0112/1+0.181/1)+(Yhat[1,1]**2+Yhat[2,1]**2-2*r*Yhat[1,1]*Yhat[2,1])*(0.181/1)*(j(2,2)-i(2));
* Næste line med varians på differencen på log skala;
*VarPred=VarDif+diag(Yhat)**2*(0.0953/1+0.0112/1+0.181/1)+exp((log(Yhat[<>,1])-log(Yhat[><,1]))**2*(2*0.181/1)*(j(2,2)-i(2)));
StdDif=sqrt(vardif);stdPred=sqrt(VarPred);
*print cov; *print jac [rowname=parmname];
jacud=j(nrow(jac),3,&i);jacud[,2:3]=jac;
if &i=1 then create jacob from jacud (|rowname=parmname|);if &i>1 then edit jacob;
setout jacob;append from jacud (|rowname=parmname|);
*print est;*print stratx;
print yHat (|format=6.1|) '|' Varcov (|format=6.1|) '|' VarDif (|format=6.1|) '|' VarPred (|format=6.1|) '|' StdDif (|format=6.1|) '|' StdPred (|format=6.1|) '|' r (|format=6.3|);
stratA=strat[,1:6];
stratb=j(2,2,0);do h=1 to 2;
  do i=7 to 12;if strat[h,i]>99 then stratb[h,1]=i-6;end;
  do i=13 to 16;if strat[h,i]>99 then stratb[h,2]=i-12;end;
  end;
stratc=strat[,17:18];stratd=strat[,19:20];
sp=vecdiag(stdPred);sdif=stdpred[1,2];CVP=sp/yhat;CVe=sqrt(vecdiag(vardif))/yhat;CVd=sdif/abs(Yhat[1]-Yhat[2]);
print strata (|format=3.|) stratb (|format=2.|) stratc (|format=4.|) stratd (|format=4.1|) yhat (|format=5.1|) sp (|format=5.1|) sdif (|format=5.1|) cvp (|format=3.2|) cve (|format=3.2|) cvd (|format=3.2|);
run;
*quit;
%end;
%mend;

options mprint symbolgen;
%regn;
/*data _null_;put '09'x;run;*/
