using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace vaccine_watch
{
    public interface IAvailabilityCheck
    {
        public Task<List<string>> GetAvailability();
    }
    public class AvailabilityCheck : IAvailabilityCheck 
    {

        private readonly IHttpClientFactory  _clientFactory ;

        public AvailabilityCheck(IHttpClientFactory  clientFactory )
        {
            _clientFactory  = clientFactory ;
        }
        public async Task<List<string>> GetAvailability()
        {
            var results = new List<string>();

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("authority","www.cvs.com");    
            client.DefaultRequestHeaders.Add("sec-ch-ua","\"Google Chrome\";v=\"89\", \"Chromium\";v=\"89\", \";Not A Brand\";v=\"99\"");    
            client.DefaultRequestHeaders.Add("sec-ch-ua-mobile","?0");    
            client.DefaultRequestHeaders.Add("user-agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36");    
            client.DefaultRequestHeaders.Add("accept","*/*");    
            client.DefaultRequestHeaders.Add("sec-fetch-site","same-origin");    
            client.DefaultRequestHeaders.Add("sec-fetch-mode","cors");    
            client.DefaultRequestHeaders.Add("sec-fetch-dest","empty");    
            client.DefaultRequestHeaders.Add("referer","https://www.cvs.com/immunizations/covid-19-vaccine?icid=cvs-home-hero1-link2-coronavirus-vaccine");    
            client.DefaultRequestHeaders.Add("accept-language","en-US,en;q=0.9");    
            client.DefaultRequestHeaders.Add("cookie","pe=p1; aat1=off-p1; acctdel_v1=on; adh_new_ps=on; adh_ps_pickup=on; adh_ps_refill=on; buynow=off; sab_displayads=on; dashboard_v1=off; db-show-allrx=on; disable-app-dynamics=on; disable-sac=on; dpp_cdc=off; dpp_drug_dir=off; dpp_sft=off; getcust_elastic=on; echomeln6=on; enable_imz=on; enable_imz_cvd=on; enable_imz_reschedule_instore=on; enable_imz_reschedule_clinic=off; flipp2=on; gbi_cvs_coupons=true; ice-phr-offer=off; v3redirecton=false; mc_cloud_service=on; mc_hl7=on; mc_home_new=on; mc_ui_ssr=off-p0; mc_videovisit=on; memberlite=on; pauth_v1=on; pivotal_forgot_password=off-p0; pivotal_sso=off-p0; pbmplaceorder=off; pbmrxhistory=on; ps=on; refill_chkbox_remove=off-p0; rxdanshownba=off; rxdfixie=on; rxd_bnr=on; rxd_dot_bnr=on; rxdpromo=on; rxduan=on; rxlite=on; rxlitelob=off; rxm=on; rxm_phone_dob=off-p1; rxm_demo_hide_LN=off; rxm_phdob_hide_LN=on; rxm_rx_challenge=off; s2c_akamaidigitizecoupon=on; s2c_beautyclub=off-p0; s2c_digitizecoupon=on; s2c_dmenrollment=off-p0; s2c_herotimer=off-p0; s2c_newcard=off-p0; s2c_papercoupon=on; s2c_persistEcCookie=on; s2c_rewardstrackerbctile=on; s2c_rewardstrackerbctenpercent=on; s2c_rewardstrackerqebtile=on; s2c_smsenrollment=on; s2cHero_lean6=on; sft_mfr_new=on; sftg=on; show_exception_status=on; v2-dash-redirection=on; ak_bmsc=A1BC3FA2960E3AC178D995A8F3896B144199128F147600000E726B60907ED439~plXPucaF4amciawJyqKg//8rPdR/CMZSSqRrwwoaCZM+2diS3RTtMduI47a9BoQv0NzfjCkR0vHZmMP+UDiQSbSdQFD4naAT/Jl9cJloz9Fl89USWNNocJdkRyE3O8hkMFlKfqkoPwEJkbBkdI55y1NXF3WkqIQC9vYMubAI+GaXmY1XeGFnlJsh4jbxSxAar0WviCBPmo+U9VNdyU6gW8sjaPA3rCf9+O/iOPwqMIYuQ=; bm_sz=34D9965C942401D97D4A45912AC44D57~YAAQjxKZQTxPDnh4AQAAWIm1owscM6GQRf5jDrJTtuUy2TABJUCaY80ycIZQEsT1LbakHlNH2ZKppXZmpcD+yOjvIyroFMYMjqaE2S9miXcsTnbYCoTqnwSwElnV1+0qWJl+7m53HK0lGNqksZjiX0YluDzXSiamhFkMnSYiIf5l9GOGZsnl/C2l17VP; mt.v=2.1714948823.1617654288308; _group1=quantum; gbi_visitorId=ckn51lhqp00013b8pmx55djpc; AMCVS_06660D1556E030D17F000101%40AdobeOrg=1; mt.cem=210103-v2-Homepage-Order - 210103-v2-Homepage-Order,control; _gcl_au=1.1.1846856036.1617654289; mp_cvs_mixpanel=%7B%22distinct_id%22%3A%20%22178a3b59330119-0d1578508d96f2-c3f3568-1fa400-178a3b5933175f%22%2C%22bc_persist_updated%22%3A%201617654289202%7D; _abck=D4418233D30D1DC45F3558C7B379B5B0~0~YAAQjxKZQVJPDnh4AQAA/Je1owVffeRl0n4XCtmVl6VOqd8ApqbenSNN2zr6qnolsQ1BHNqORKgJaTNvKnJig8W3yRZwEZZPzf7A3Ar0c2T/4BUVrwQ/AodSq7nMn0Fi+s7LsFlxoaY1BCzj5Eaa2L42AT7G4fW12pb53K/7p8WCoFUt5XvTX4ebCJw4d7Tt6xlSt+XFAEExeyGf2s///ddDZwRt54WbWpg8NmgiPUeHzn0JE1yZc4Rp/6KuhvRGv4aRbqcTzbmUPwbdPGIdVS2BHOGsAi9/6a3m/pTU9u4KGhq3LDZELxViqskkpcod8Sv0OTwSA0xYHMFc6ZLLaeg59im4/eDFnKaWi3vtcPWMa7jln5bwEm3MgYoYGHVGwKE4WV5my0Jh/OYeAXzOnL9t3Zzc~-1~||-1||~-1; _4c_mc_=742be169-ecf8-4263-a979-329e436688a4; QuantumMetricSessionID=cec15bd7633c833238f1a63af058a9aa; QuantumMetricUserID=06cdefd1e57d8e34fe8681d75f911cd6; QuantumMetricSessionLink=https://cvs.quantummetric.com/#/users/search?autoreplay=true&qmsessioncookie=cec15bd7633c833238f1a63af058a9aa&ts=1617611090-1617697490; AMCV_06660D1556E030D17F000101%40AdobeOrg=-330454231%7CMCIDTS%7C18723%7CMCMID%7C83405588685736787494593825988057169386%7CMCAAMLH-1618259089%7C9%7CMCAAMB-1618259089%7CRKhpRz8krg2tLO6pguXWp5olkAcUniQYPHaMWWgdJ3xzPWQmdj0y%7CMCOPTOUT-1617661489s%7CNONE%7CMCAID%7CNONE%7CvVersion%7C3.1.2; s_cc=true; gpv_p10=www.cvs.com%2Fimmunizations%2Fcovid-19-vaccine; akavpau_www_cvs_com_general=1617655611~id=291beb81b06dfe1a41436d5a964b4aab; CVPF=38vkqWE4yPDr74bUYQTx1e9df7i1Ve91r9AUmG7YAF2Hlt7X6blLiOw; bm_sv=873F38EF8C465B25C40D4D577FF2070A~ABdvYI/ZOqk9otRpVW/u2fgHzYrLFnr8OYVd8LzfFdn9pGIIeNTW1G1QClbQiVrltduEG38xjodB6nDvprTS6ibJW9pFtx+bR0J/eOmowcVpql11KMJt7Fi9bAEbI4Eyv9KOVd5ljqb7iDHUE0jk9A==; gpv_e5=cvs%7Cdweb%7Cimmunizations%7Ccovid-19-vaccine%7Cpromo%3A%20covid-19%20vaccines%20in%20minnesota%20modal; RT=\"z=1&dm=cvs.com&si=17763fc4-a0e9-4ba9-804d-4808a0873756&ss=kn51lgzh&sl=3&tt=5bx&bcn=%2F%2F173e2548.akstat.io%2F&nu=ifz4dlr5&cl=1rk2b\"; qmexp=1617659053264; s_sq=%5B%5BB%5D%5D; utag_main=v_id:0178a3b590750049daa69655f46803073001406b00bd0$_sn:1$_ss:0$_pn:3%3Bexp-session$_st:1617659053414$ses_id:1617654288501%3Bexp-session$vapi_domain:cvs.com");         

            try
            {
                var cvsResult = await client.GetAsync("https://www.cvs.com/immunizations/covid-19-vaccine.vaccine-status.MN.json?vaccineinfo");
                var responseContent = await cvsResult.Content.ReadAsStringAsync();
                var payload = JsonConvert.DeserializeObject<Root>(responseContent); 

                foreach(var availability in payload.responsePayloadData.data.MN)
                {
                    if(availability.status == "Available")
                    {
                        results.Add(availability.city);
                    }
                }

                return results;
            }
            catch
            {
                throw;
            }
            
        }
    }
}