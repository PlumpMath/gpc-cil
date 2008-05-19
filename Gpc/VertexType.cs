namespace Gpc
{
	public enum VertexType
	{
		NUL =  0,  /* Empty non-intersection            */
		EMX =  1,  /* External maximum                  */
		ELI =  2,  /* External left intermediate        */
		TED =  3,  /* Top edge                          */
		ERI =  4,  /* External right intermediate       */
		RED =  5,  /* Right edge                        */
		IMM =  6,  /* Internal maximum and minimum      */
		IMN =  7,  /* Internal minimum                  */
		EMN =  8,  /* External minimum                  */
		EMM =  9,  /* External maximum and minimum      */
		LED = 10,  /* Left edge                         */
		ILI = 11,  /* Internal left intermediate        */
		BED = 12,  /* Bottom edge                       */
		IRI = 13,  /* Internal right intermediate       */
		IMX = 14,  /* Internal maximum                  */
		FUL = 15,  /* Full non-intersection             */
	}
}
