using System.Xml.Serialization;
using System.Collections.Generic;

namespace AddOnFE_Facturatech.Proveedor.Facturatech
{
    public abstract class DocumentoBase
    {
        [XmlElement("ENC")]
        public ENC ENC { get; set; }

        [XmlElement("EMI")]
        public EMI EMI { get; set; }

        [XmlElement("ADQ")]
        public ADQ ADQ { get; set; }

        [XmlElement("TOT")]
        public TOT TOT { get; set; }

        [XmlElement("TIM")]
        public List<TIM> TIMs { get; set; }

        [XmlElement("DRF")]
        public DRF DRF { get; set; }

        [XmlElement("MEP")]
        public MEP MEP { get; set; }

        [XmlElement("ITE")]
        public List<ITE> ITEs { get; set; }

        public DocumentoBase()
        {
            TIMs = new List<TIM>();
            ITEs = new List<ITE>();
        }
    }

    [XmlRoot("FACTURA")]
    public class Factura
    {
        [XmlElement("ENC")]
        public ENC ENC { get; set; }

        [XmlElement("EMI")]
        public EMI EMI { get; set; }

        [XmlElement("ADQ")]
        public ADQ ADQ { get; set; }

        [XmlElement("TOT")]
        public TOT TOT { get; set; }

        [XmlElement("TIM")]
        public List<TIM> TIMs { get; set; }

        [XmlElement("DRF")]
        public DRF DRF { get; set; }

        [XmlElement("MEP")]
        public MEP MEP { get; set; }

        [XmlElement("ITE")]
        public List<ITE> ITEs { get; set; }

        public Factura()
        {
            TIMs = new List<TIM>();
            ITEs = new List<ITE>();
        }
    }

    [XmlRoot("NOTA")]
    public class Nota
    {
        [XmlElement("ENC")]
        public ENC ENC { get; set; }

        [XmlElement("EMI")]
        public EMI EMI { get; set; }

        [XmlElement("ADQ")]
        public ADQ ADQ { get; set; }

        [XmlElement("TOT")]
        public TOT TOT { get; set; }

        [XmlElement("TIM")]
        public List<TIM> TIMs { get; set; }

        [XmlElement("DRF")]
        public DRF DRF { get; set; }

        [XmlElement("MEP")]
        public MEP MEP { get; set; }

        [XmlElement("ITE")]
        public List<ITE> ITEs { get; set; }

        public Nota()
        {
            TIMs = new List<TIM>();
            ITEs = new List<ITE>();
        }
    }

    [XmlRoot("DOCUMENTO_SOPORTE")]
    public class DocSoporte 
    {
        [XmlElement("ENC")]
        public ENC ENC { get; set; }

        [XmlElement("EMI")]
        public EMI EMI { get; set; }

        [XmlElement("ADQ")]
        public ADQ ADQ { get; set; }

        [XmlElement("TOT")]
        public TOT TOT { get; set; }

        [XmlElement("TIM")]
        public List<TIM> TIMs { get; set; }

        [XmlElement("DRF")]
        public DRF DRF { get; set; }

        [XmlElement("MEP")]
        public MEP MEP { get; set; }

        [XmlElement("ITE")]
        public List<ITE> ITEs { get; set; }

        public DocSoporte()
        {
            TIMs = new List<TIM>();
            ITEs = new List<ITE>();
        }
    }

    public class ENC
    {
        [XmlElement("ENC_1")]
        public string ENC1 { get; set; }

        [XmlElement("ENC_2")]
        public string ENC2 { get; set; }

        [XmlElement("ENC_3")]
        public string ENC3 { get; set; }

        [XmlElement("ENC_4")]
        public string ENC4 { get; set; }

        [XmlElement("ENC_5")]
        public string ENC5 { get; set; }

        [XmlElement("ENC_6")]
        public string ENC6 { get; set; }

        [XmlElement("ENC_9")]
        public string ENC9 { get; set; }

        [XmlElement("ENC_10")]
        public string ENC10 { get; set; }

        [XmlElement("ENC_15")]
        public string ENC15 { get; set; }

        [XmlElement("ENC_20")]
        public string ENC20 { get; set; }

        [XmlElement("ENC_21")]
        public string ENC21 { get; set; }
    }

    public class EMI
    {
        [XmlElement("EMI_1")]
        public string EMI1 { get; set; }

        [XmlElement("EMI_2")]
        public string EMI2 { get; set; }

        [XmlElement("EMI_3")]
        public string EMI3 { get; set; }

        [XmlElement("EMI_6")]
        public string EMI6 { get; set; }

        [XmlElement("EMI_7")]
        public string EMI7 { get; set; }

        [XmlElement("EMI_10")]
        public string EMI10 { get; set; }

        [XmlElement("EMI_11")]
        public string EMI11 { get; set; }

        [XmlElement("EMI_13")]
        public string EMI13 { get; set; }

        [XmlElement("EMI_15")]
        public string EMI15 { get; set; }

        [XmlElement("EMI_19")]
        public string EMI19 { get; set; }

        [XmlElement("EMI_22")]
        public string EMI22 { get; set; }

        [XmlElement("EMI_23")]
        public string EMI23 { get; set; }

        [XmlElement("EMI_24")]
        public string EMI24 { get; set; }

        [XmlElement("TAC")]
        public TAC TAC { get; set; }

        [XmlElement("DFE")]
        public DFE DFE { get; set; }

        [XmlElement("ICC")]
        public ICC ICC { get; set; }

        [XmlElement("CDE")]
        public CDE CDE { get; set; }

        [XmlElement("GTE")]
        public GTE GTE { get; set; }
    }

    public class ADQ
    {
        [XmlElement("ADQ_1")]
        public string ADQ1 { get; set; }

        [XmlElement("ADQ_2")]
        public string ADQ2 { get; set; }

        [XmlElement("ADQ_3")]
        public string ADQ3 { get; set; }

        [XmlElement("ADQ_6")]
        public string ADQ6 { get; set; }

        [XmlElement("ADQ_7")]
        public string ADQ7 { get; set; }

        [XmlElement("ADQ_10")]
        public string ADQ10 { get; set; }

        [XmlElement("ADQ_11")]
        public string ADQ11 { get; set; }

        [XmlElement("ADQ_13")]
        public string ADQ13 { get; set; }

        [XmlElement("ADQ_14")]
        public string ADQ14 { get; set; }

        [XmlElement("ADQ_15")]
        public string ADQ15 { get; set; }

        [XmlElement("ADQ_19")]
        public string ADQ19 { get; set; }

        [XmlElement("ADQ_21")]
        public string ADQ21 { get; set; }

        [XmlElement("ADQ_22")]
        public string ADQ22 { get; set; }

        [XmlElement("ADQ_23")]
        public string ADQ23 { get; set; }

        [XmlElement("TCR")]
        public TCR TCR { get; set; }

        [XmlElement("ILA")]
        public ILA ILA { get; set; }

        [XmlElement("DFA")]
        public DFA DFA { get; set; }

        [XmlElement("ICR")]
        public ICR ICR { get; set; }

        [XmlElement("CDA")]
        public CDA CDA { get; set; }

        [XmlElement("GTA")]
        public GTA GTA { get; set; }
    }

    public class TOT
    {
        [XmlElement("TOT_1")]
        public string TOT1 { get; set; }

        [XmlElement("TOT_2")]
        public string TOT2 { get; set; }

        [XmlElement("TOT_3")]
        public string TOT3 { get; set; }

        [XmlElement("TOT_4")]
        public string TOT4 { get; set; }

        [XmlElement("TOT_5")]
        public string TOT5 { get; set; }

        [XmlElement("TOT_6")]
        public string TOT6 { get; set; }

        [XmlElement("TOT_7")]
        public string TOT7 { get; set; }

        [XmlElement("TOT_8")]
        public string TOT8 { get; set; }
    }

    public class TIM
    {
        [XmlElement("TIM_1")]
        public string TIM1 { get; set; }

        [XmlElement("TIM_2")]
        public string TIM2 { get; set; }

        [XmlElement("TIM_3")]
        public string TIM3 { get; set; }

        [XmlElement("IMP")]
        public List<IMP> IMPs { get; set; }
        public TIM()
        {
            IMPs = new List<IMP>();
        }
    }

    public class IMP
    {
        [XmlElement("IMP_1")]
        public string IMP1 { get; set; }

        [XmlElement("IMP_2")]
        public string IMP2 { get; set; }

        [XmlElement("IMP_3")]
        public string IMP3 { get; set; }

        [XmlElement("IMP_4")]
        public string IMP4 { get; set; }

        [XmlElement("IMP_5")]
        public string IMP5 { get; set; }

        [XmlElement("IMP_6")]
        public string IMP6 { get; set; }
    }

    public class DRF
    {
        [XmlElement("DRF_1")]
        public string DRF1 { get; set; }

        [XmlElement("DRF_2")]
        public string DRF2 { get; set; }

        [XmlElement("DRF_3")]
        public string DRF3 { get; set; }

        [XmlElement("DRF_4")]
        public string DRF4 { get; set; }

        [XmlElement("DRF_5")]
        public string DRF5 { get; set; }

        [XmlElement("DRF_6")]
        public string DRF6 { get; set; }
    }

    public class MEP
    {
        [XmlElement("MEP_1")]
        public string MEP1 { get; set; }

        [XmlElement("MEP_2")]
        public string MEP2 { get; set; }

        [XmlElement("MEP_3")]
        public string MEP3 { get; set; }
    }

    public class ITE
    {
        [XmlElement("ITE_1")]
        public string ITE1 { get; set; }

        [XmlElement("ITE_3")]
        public string ITE3 { get; set; }

        [XmlElement("ITE_4")]
        public string ITE4 { get; set; }

        [XmlElement("ITE_5")]
        public string ITE5 { get; set; }

        [XmlElement("ITE_6")]
        public string ITE6 { get; set; }

        [XmlElement("ITE_7")]
        public string ITE7 { get; set; }

        [XmlElement("ITE_8")]
        public string ITE8 { get; set; }

        [XmlElement("ITE_11")]
        public string ITE11 { get; set; }

        [XmlElement("ITE_20")]
        public string ITE20 { get; set; }

        [XmlElement("ITE_21")]
        public string ITE21 { get; set; }

        [XmlElement("ITE_24")]
        public string ITE24 { get; set; }

        [XmlElement("ITE_27")]
        public string ITE27 { get; set; }

        [XmlElement("ITE_28")]
        public string ITE28 { get; set; }

        [XmlElement("IAE")]
        public IAE IAE { get; set; }

        [XmlElement("IDE")]
        public IDE IDE { get; set; }

        [XmlElement("TII")]
        public List<TII> TIIs { get; set; }

        public ITE()
        {
            TIIs = new List<TII>();
        }
    }

    public class TAC
    {
        [XmlElement("TAC_1")]
        public string TAC1 { get; set; }
    }

    public class DFE
    {
        [XmlElement("DFE_1")]
        public string DFE1 { get; set; }

        [XmlElement("DFE_2")]
        public string DFE2 { get; set; }

        [XmlElement("DFE_3")]
        public string DFE3 { get; set; }

        [XmlElement("DFE_4")]
        public string DFE4 { get; set; }

        [XmlElement("DFE_5")]
        public string DFE5 { get; set; }

        [XmlElement("DFE_6")]
        public string DFE6 { get; set; }

        [XmlElement("DFE_7")]
        public string DFE7 { get; set; }

        [XmlElement("DFE_8")]
        public string DFE8 { get; set; }
    }

    public class ICC
    {
        [XmlElement("ICC_1")]
        public string ICC1 { get; set; }

        [XmlElement("ICC_9")]
        public string ICC9 { get; set; }
    }

    public class CDE
    {
        [XmlElement("CDE_1")]
        public string CDE1 { get; set; }

        [XmlElement("CDE_2")]
        public string CDE2 { get; set; }

        [XmlElement("CDE_3")]
        public string CDE3 { get; set; }

        [XmlElement("CDE_4")]
        public string CDE4 { get; set; }
    }

    public class GTE
    {
        [XmlElement("GTE_1")]
        public string GTE1 { get; set; }

        [XmlElement("GTE_2")]
        public string GTE2 { get; set; }
    }

    public class TCR
    {
        [XmlElement("TCR_1")]
        public string TCR1 { get; set; }
    }

    public class ILA
    {
        [XmlElement("ILA_1")]
        public string ILA1 { get; set; }

        [XmlElement("ILA_2")]
        public string ILA2 { get; set; }

        [XmlElement("ILA_3")]
        public string ILA3 { get; set; }

        [XmlElement("ILA_4")]
        public string ILA4 { get; set; }
    }

    public class DFA
    {
        [XmlElement("DFA_1")]
        public string DFA1 { get; set; }

        [XmlElement("DFA_2")]
        public string DFA2 { get; set; }

        [XmlElement("DFA_3")]
        public string DFA3 { get; set; }

        [XmlElement("DFA_4")]
        public string DFA4 { get; set; }

        [XmlElement("DFA_5")]
        public string DFA5 { get; set; }

        [XmlElement("DFA_6")]
        public string DFA6 { get; set; }

        [XmlElement("DFA_7")]
        public string DFA7 { get; set; }

        [XmlElement("DFA_8")]
        public string DFA8 { get; set; }

    }

    public class ICR
    {
        [XmlElement("ICR_1")]
        public string ICR1 { get; set; }
    }

    public class CDA
    {
        [XmlElement("CDA_1")]
        public string CDA1 { get; set; }

        [XmlElement("CDA_2")]
        public string CDA2 { get; set; }

        [XmlElement("CDA_3")]
        public string CDA3 { get; set; }

        [XmlElement("CDA_4")]
        public string CDA4 { get; set; }

    }

    public class GTA
    {
        [XmlElement("GTA_1")]
        public string GTA1 { get; set; }

        [XmlElement("GTA_2")]
        public string GTA2 { get; set; }

    }

    public class IAE
    {
        [XmlElement("IAE_1")]
        public string IAE1 { get; set; }

        [XmlElement("IAE_2")]
        public string IAE2 { get; set; }
    }

    public class IDE
    {
        [XmlElement("IDE_1")]
        public string IDE1 { get; set; }

        [XmlElement("IDE_2")]
        public string IDE2 { get; set; }

        [XmlElement("IDE_3")]
        public string IDE3 { get; set; }

        [XmlElement("IDE_6")]
        public string IDE6 { get; set; }

        [XmlElement("IDE_7")]
        public string IDE7 { get; set; }

        [XmlElement("IDE_8")]
        public string IDE8 { get; set; }

        [XmlElement("IDE_10")]
        public string IDE10 { get; set; }
    }

    public class TII
    {
        [XmlElement("TII_1")]
        public string TII1 { get; set; }

        [XmlElement("TII_2")]
        public string TII2 { get; set; }

        [XmlElement("TII_3")]
        public string TII3 { get; set; }

        [XmlElement("IIM")]
        public IIM IIM { get; set; }
    }

    public class IIM
    {
        [XmlElement("IIM_1")]
        public string IIM1 { get; set; }

        [XmlElement("IIM_2")]
        public string IIM2 { get; set; }

        [XmlElement("IIM_3")]
        public string IIM3 { get; set; }

        [XmlElement("IIM_4")]
        public string IIM4 { get; set; }

        [XmlElement("IIM_5")]
        public string IIM5 { get; set; }

        [XmlElement("IIM_6")]
        public string IIM6 { get; set; }
    }


}
