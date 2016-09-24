
public class Fraction {	
		long m_iNumerator;
		long m_iDenominator;
		
		
		public Fraction() throws Exception
		{
			Initialize(0,1);
		}
	
		public Fraction(long iWholeNumber) throws Exception
		{
			Initialize(iWholeNumber, 1);
		}
		
		public Fraction(String strValue) throws Exception
		{
			Fraction temp=ToFraction(strValue);
			Initialize(temp.getNumerator(), temp.getDenominator());
		}
		
		public Fraction(long iNumerator, long iDenominator) throws Exception
		{
			Initialize(iNumerator, iDenominator);
		}
		private void Initialize(long iNumerator, long iDenominator) throws Exception
		{
			this.setNumerator(iNumerator);
			this.setDenominator(iDenominator);
			ReduceFraction(this);
		}
	
	
	    
		public long getNumerator()
		{
			return m_iNumerator;	
		}
		public void setNumerator(long Numerator)
		{
			m_iNumerator = Numerator;	
		}
		public long getDenominator() throws Exception
		{
			if(m_iDenominator != 0)
			  return m_iDenominator;
			else
				throw new Exception("Denominator cannot be assigned a ZERO Value");
		}
		public void setDenominator(long Denominator) throws Exception
		{
			if(Denominator != 0)
			 m_iDenominator = Denominator;
			else
				throw new Exception("Denominator cannot be assigned a ZERO Value");
		}
	
	

		
		public String ToString() throws Exception
		{
			String str;
			if ( this.getDenominator()==1 )
				str=Long.toString(this.getNumerator());
			else
				str=this.getNumerator() + "/" + this.getDenominator();
			return str;
		}
	
		public static Fraction ToFraction(String strValue) throws Exception
		{
			int i;
			for (i=0;i<strValue.length();i++)
				if (strValue.charAt(i)=='/')
					break;
			
			if (i==strValue.length())		
				return new Fraction(Long.parseLong(strValue),1);
		
			long iNumerator=Long.parseLong(strValue.substring(0,i));
			long iDenominator=Long.parseLong(strValue.substring(i+1));
			return new Fraction(iNumerator, iDenominator);
		}
		
		public static void ReduceFraction(Fraction frac) throws Exception
		{
			try
			{
				if (frac.getNumerator()==0)
				{
					frac.setDenominator(1);
					return;
				}
				
				long iGCD=GCD(frac.getNumerator(), frac.getDenominator());
				frac.setNumerator(frac.getNumerator()/iGCD);
				frac.setDenominator(frac.getDenominator()/iGCD);
				
				if ( frac.getDenominator()<0 )	
				{
					frac.setNumerator(frac.getNumerator()*(-1));
					frac.setDenominator(frac.getDenominator()*(-1));	
				}
			} 
			catch(Exception exp)
			{
				throw new Exception("Cannot reduce Fraction: " + exp.getMessage());
			}
		}
		private static long GCD(long iNo1, long iNo2)
		{
		
			if (iNo1 < 0) iNo1 = -iNo1;
			if (iNo2 < 0) iNo2 = -iNo2;
			
			do
			{
				if (iNo1 < iNo2)
				{
					long tmp = iNo1;  
					iNo1 = iNo2;
					iNo2 = tmp;
				}
				iNo1 = iNo1 % iNo2;
			} while (iNo1 != 0);
			return iNo2;
		}
	
		public static Fraction Inverse(Fraction frac1) throws Exception 
		{
			if (frac1.getNumerator()==0)
				throw new Exception("Operation not possible (Denominator cannot be assigned a ZERO Value)");
			long iNumerator=frac1.getDenominator();
			long iDenominator=frac1.getNumerator();
			return ( new Fraction(iNumerator, iDenominator));
		}	
		
		public String Add(String str1, String str2)
		{
			
			try{
			  Fraction frac1 = new Fraction(str1);
			  Fraction frac2 = new Fraction(str2);
			  long iNumerator=frac1.getNumerator()*frac2.getDenominator() + frac2.getNumerator()*frac1.getDenominator();
			  long iDenominator=frac1.getDenominator()*frac2.getDenominator();
			  return ( new Fraction(iNumerator, iDenominator).ToString() );	
			}catch(Exception e){
				return e.getMessage();
			}
		}
		
		public String Multiply(String str1, String str2)
		{		
			try{
			  Fraction frac1 = new Fraction(str1);
			  Fraction frac2 = new Fraction(str2);
			  long iNumerator=frac1.getNumerator()*frac2.getNumerator();
				long iDenominator=frac1.getDenominator()*frac2.getDenominator();
			  return ( new Fraction(iNumerator, iDenominator).ToString() );	
			}catch(Exception e){
				return e.getMessage();
			}
		}
}
