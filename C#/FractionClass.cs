using System;

namespace Fra
{
	
	public class Fraction
	{
		
		long m_iNumerator;
		long m_iDenominator;
		
		
		public Fraction()
		{
			Initialize(0,1);
		}
	
		public Fraction(long iWholeNumber)
		{
			Initialize(iWholeNumber, 1);
		}
	
		public Fraction(double dDecimalValue)
		{
			Fraction temp=ToFraction(dDecimalValue);
			Initialize(temp.Numerator, temp.Denominator);
		}
		
		public Fraction(string strValue)
		{
			Fraction temp=ToFraction(strValue);
			Initialize(temp.Numerator, temp.Denominator);
		}
		
		public Fraction(long iNumerator, long iDenominator)
		{
			Initialize(iNumerator, iDenominator);
		}
		
		
		private void Initialize(long iNumerator, long iDenominator)
		{
			Numerator=iNumerator;
			Denominator=iDenominator;
			ReduceFraction(this);
		}
	
		
		public long Denominator
		{
			get
			{	return m_iDenominator;	}
			set
			{
				if (value!=0)
					m_iDenominator=value;
				else
					throw new FractionException("Denominator cannot be assigned a ZERO Value");
			}
		}
	
		public long Numerator
		{
			get	
			{	return m_iNumerator;	}
			set
			{	m_iNumerator=value;	}
		}
	

		public double ToDouble()
		{
			return ( (double)this.Numerator/this.Denominator );
		}

		
		public override string ToString()
		{
			string str;
			if ( this.Denominator==1 )
				str=this.Numerator.ToString();
			else
				str=this.Numerator + "/" + this.Denominator;
			return str;
		}
	
		public static Fraction ToFraction(string strValue)
		{
			int i;
			for (i=0;i<strValue.Length;i++)
				if (strValue[i]=='/')
					break;
			
			if (i==strValue.Length)		
				return ( Convert.ToDouble(strValue));
			
		
			long iNumerator=Convert.ToInt64(strValue.Substring(0,i));
			long iDenominator=Convert.ToInt64(strValue.Substring(i+1));
			return new Fraction(iNumerator, iDenominator);
		}
		
		
		
		public static Fraction ToFraction(double dValue)
		{
			try
			{
				checked
				{
					Fraction frac;
					if (dValue%1==0)	// if whole number
					{
						frac=new Fraction( (long) dValue );
					}
					else
					{
						double dTemp=dValue;
						long iMultiple=1;
						string strTemp=dValue.ToString();
						while ( strTemp.IndexOf("E")>0 )	// if in the form like 12E-9
						{
							dTemp*=10;
							iMultiple*=10;
							strTemp=dTemp.ToString();
						}
						int i=0;
						while ( strTemp[i]!='.' )
							i++;
						int iDigitsAfterDecimal=strTemp.Length-i-1;
						while ( iDigitsAfterDecimal>0  )
						{
							dTemp*=10;
							iMultiple*=10;
							iDigitsAfterDecimal--;
						}
						frac=new Fraction( (int)Math.Round(dTemp) , iMultiple );
					}
					return frac;
				}
			}
			catch(OverflowException)
			{
				throw new FractionException("Conversion not possible due to overflow");
			}
			catch(Exception)
			{
				throw new FractionException("Conversion not possible");
			}
		}

		public static Fraction Inverse(Fraction frac1)
		{
			if (frac1.Numerator==0)
				throw new FractionException("Operation not possible (Denominator cannot be assigned a ZERO Value)");
	
			long iNumerator=frac1.Denominator;
			long iDenominator=frac1.Numerator;
			return ( new Fraction(iNumerator, iDenominator));
		}	
	

	
		public static Fraction operator -(Fraction frac1)
		{	return ( Negate(frac1) );	}
	                              
		public static Fraction operator +(Fraction frac1, Fraction frac2)
		{	return ( Add(frac1 , frac2) );	}
	
		public static Fraction operator +(int iNo, Fraction frac1)
		{	return ( Add(frac1 , new Fraction(iNo) ) );	}
	
		public static Fraction operator +(Fraction frac1, int iNo)
		{	return ( Add(frac1 , new Fraction(iNo) ) );	}

		public static Fraction operator +(double dbl, Fraction frac1)
		{	return ( Add(frac1 , Fraction.ToFraction(dbl) ) );	}
	
		public static Fraction operator +(Fraction frac1, double dbl)
		{	return ( Add(frac1 , Fraction.ToFraction(dbl) ) );	}
	
		public static Fraction operator -(Fraction frac1, Fraction frac2)
		{	return ( Add(frac1 , -frac2) );	}
	
		public static Fraction operator -(int iNo, Fraction frac1)
		{	return ( Add(-frac1 , new Fraction(iNo) ) );	}
	
		public static Fraction operator -(Fraction frac1, int iNo)
		{	return ( Add(frac1 , -(new Fraction(iNo)) ) );	}

		public static Fraction operator -(double dbl, Fraction frac1)
		{	return ( Add(-frac1 , Fraction.ToFraction(dbl) ) );	}
	
		public static Fraction operator -(Fraction frac1, double dbl)
		{	return ( Add(frac1 , -Fraction.ToFraction(dbl) ) );	}
	
		public static Fraction operator *(Fraction frac1, Fraction frac2)
		{	return ( Multiply(frac1 , frac2) );	}
	
		public static Fraction operator *(int iNo, Fraction frac1)
		{	return ( Multiply(frac1 , new Fraction(iNo) ) );	}
	
		public static Fraction operator *(Fraction frac1, int iNo)
		{	return ( Multiply(frac1 , new Fraction(iNo) ) );	}
	
		public static Fraction operator *(double dbl, Fraction frac1)
		{	return ( Multiply(frac1 , Fraction.ToFraction(dbl) ) );	}
	
		public static Fraction operator *(Fraction frac1, double dbl)
		{	return ( Multiply(frac1 , Fraction.ToFraction(dbl) ) );	}
	
		public static Fraction operator /(Fraction frac1, Fraction frac2)
		{	return ( Multiply( frac1 , Inverse(frac2) ) );	}
	
		public static Fraction operator /(int iNo, Fraction frac1)
		{	return ( Multiply( Inverse(frac1) , new Fraction(iNo) ) );	}
	
		public static Fraction operator /(Fraction frac1, int iNo)
		{	return ( Multiply( frac1 , Inverse(new Fraction(iNo)) ) );	}
	
		public static Fraction operator /(double dbl, Fraction frac1)
		{	return ( Multiply( Inverse(frac1) , Fraction.ToFraction(dbl) ) );	}
	
		public static Fraction operator /(Fraction frac1, double dbl)
		{	return ( Multiply( frac1 , Fraction.Inverse( Fraction.ToFraction(dbl) ) ) );	}

		public static bool operator ==(Fraction frac1, Fraction frac2)
		{	return frac1.Equals(frac2);		}

		public static bool operator !=(Fraction frac1, Fraction frac2)
		{	return ( !frac1.Equals(frac2) );	}

		public static bool operator ==(Fraction frac1, int iNo)
		{	return frac1.Equals( new Fraction(iNo));	}

		public static bool operator !=(Fraction frac1, int iNo)
		{	return ( !frac1.Equals( new Fraction(iNo)) );	}
		
		public static bool operator ==(Fraction frac1, double dbl)
		{	return frac1.Equals( new Fraction(dbl));	}

		public static bool operator !=(Fraction frac1, double dbl)
		{	return ( !frac1.Equals( new Fraction(dbl)) );	}
		
		public static bool operator<(Fraction frac1, Fraction frac2)
		{	return frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator;	}

		public static bool operator>(Fraction frac1, Fraction frac2)
		{	return frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator;	}

		public static bool operator<=(Fraction frac1, Fraction frac2)
		{	return frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator;	}
		
		public static bool operator>=(Fraction frac1, Fraction frac2)
		{	return frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator;	}
		
		
		
		public static implicit operator Fraction(long lNo)
		{	return new Fraction(lNo);	}
		public static implicit operator Fraction(double dNo)
		{	return new Fraction(dNo);	}
		public static implicit operator Fraction(string strNo)
		{	return new Fraction(strNo);	}

		public static explicit operator double(Fraction frac)
		{	return frac.ToDouble();	}

		public static implicit operator string(Fraction frac)
		{	return frac.ToString();	}
		
		
		public override bool Equals(object obj)
		{
			Fraction frac=(Fraction)obj;
			return ( Numerator==frac.Numerator && Denominator==frac.Denominator);
		}
		
	
   		public override int GetHashCode()
   		{
			return ( Convert.ToInt32((Numerator ^ Denominator) & 0xFFFFFFFF) ) ;
		}

		
		private static Fraction Negate(Fraction frac1)
		{
			long iNumerator=-frac1.Numerator;
			long iDenominator=frac1.Denominator;
			return ( new Fraction(iNumerator, iDenominator) );

		}	

		private static Fraction Add(Fraction frac1, Fraction frac2)
		{
			try
			{
				checked
				{
					long iNumerator=frac1.Numerator*frac2.Denominator + frac2.Numerator*frac1.Denominator;
					long iDenominator=frac1.Denominator*frac2.Denominator;
					return ( new Fraction(iNumerator, iDenominator) );
				}
			}
			catch(OverflowException)
			{
				throw new FractionException("Overflow occurred while performing arithemetic operation");
			}
			catch(Exception)
			{
				throw new FractionException("An error occurred while performing arithemetic operation");
			}
		}
	
		private static Fraction Multiply(Fraction frac1, Fraction frac2)
		{
			try
			{
				checked
				{
					long iNumerator=frac1.Numerator*frac2.Numerator;
					long iDenominator=frac1.Denominator*frac2.Denominator;
					return ( new Fraction(iNumerator, iDenominator) );
				}
			}
			catch(OverflowException)
			{
				throw new FractionException("Overflow occurred while performing arithemetic operation");
			}
			catch(Exception)
			{
				throw new FractionException("An error occurred while performing arithemetic operation");
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
	
		
		public static void ReduceFraction(Fraction frac)
		{
			try
			{
				if (frac.Numerator==0)
				{
					frac.Denominator=1;
					return;
				}
				
				long iGCD=GCD(frac.Numerator, frac.Denominator);
				frac.Numerator/=iGCD;
				frac.Denominator/=iGCD;
				
				if ( frac.Denominator<0 )	
				{
					frac.Numerator*=-1;
					frac.Denominator*=-1;	
				}
			} 
			catch(Exception exp)
			{
				throw new FractionException("Cannot reduce Fraction: " + exp.Message);
			}
		}
			
	}	
	public class FractionException : Exception
	{
		public FractionException() : base()
		{}
	
		public FractionException(string Message) : base(Message)
		{}
		
		public FractionException(string Message, Exception InnerException) : base(Message, InnerException)
		{}
	}	
	

}	