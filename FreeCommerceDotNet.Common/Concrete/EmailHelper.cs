﻿using System;

namespace FreeCommerceDotNet.Common.Concrete
{
    public static class EmailHelper
    {
        public static string RegisterSuccessMail()
        {
            string htmlString =
                "<!DOCTYPE html> <html lang=\"en\">     <head>         <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">         <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">         <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">         <link rel=\"icon\" href=\"~/assets/images/favicon/1.png\" type=\"image/x-icon\">         <link rel=\"shortcut icon\" href=\"~/assets/images/favicon/1.png\" type=\"image/x-icon\">         <title>Multikart | Email template </title>         <link href=\"https://fonts.googleapis.com/css?family=Lato:300,400,700,900\" rel=\"stylesheet\">          <style type=\"text/css\">             body{             \ttext-align: center;             \tmargin: 0 auto;             \twidth: 650px;             \tfont-family: 'Lato', sans-serif;             \tbackground-color: #e2e2e2;\t\t      \t             \tdisplay: block;             }             ul{             \tmargin:0;             \tpadding: 0;             }             li{             \tdisplay: inline-block;             \ttext-decoration: unset;             }             a{             \ttext-decoration: none;             }             h5{             \tmargin:10px;             \tcolor:#777;             }             .text-center{             \ttext-align: center             }             .main-bg-light{             \tbackground-color: #fafafa;             }             .title{             \tcolor: #444444;             \tfont-size: 22px;             \tfont-weight: bold;             \tmargin-top: 0px;             \tmargin-bottom: 10px;             \tpadding-bottom: 0;             \ttext-transform: uppercase;             \tdisplay: inline-block;             \tline-height: 1;             }             .menu{             width:100%;             }             .menu li a{             \ttext-transform: capitalize;             \tcolor:#444;             \tfont-size:16px;             \tmargin-right:15px             }             .main-logo{             \twidth: 180px;             \tpadding: 10px 20px;                 margin-bottom: -5px;             }             .product-box .product { \t            text-align: center; \t            position: relative;         \t}             .product-info {             \tmargin-top: 15px;             }                        .product-info h6 {             \tline-height: 1;             \tmargin-bottom: 0;             \tpadding-bottom: 5px;             \tfont-size: 14px;             \tfont-family: \"Open Sans\", sans-serif;             \tcolor: #777;             \tmargin-top: 0;             }             .product-info h4 {             \tfont-size: 16px;             \tcolor: #444;             \tfont-weight: 700;             \tmargin-bottom: 0;             \tmargin-top: 5px;             \tpadding-bottom: 5px;             \tline-height: 1;             }             .footer-social-icon tr td img{             \tmargin-left:5px;             \tmargin-right:5px;             }         </style>     </head>     <body style=\"margin: 20px auto;\">         <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color: #fff; -webkit-box-shadow: 0px 0px 14px -4px rgba(0, 0, 0, 0.2705882353);box-shadow: 0px 0px 14px -4px rgba(0, 0, 0, 0.2705882353);\">             <tbody>                 <tr>                     <td>                         <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">                             <tr class=\"header\">                                 <td align=\"left\" valign=\"top\" >                                     <img src=\"~/assets/images/email-temp/logo.png\" class=\"main-logo\">                                 </td>                                 <td class=\"menu\" align=\"right\">                                     <ul>                                         <li><a href=\"#\">Home</a></li>                                         <li><a href=\"#\">Whishlist</a></li>                                         <li><a href=\"#\">my cart</a></li>                                         <li><a href=\"#\">Contact</a></li>                                     </ul>                                 </td>                             </tr>                         </table>                         <table class=\"slider\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">                             <tr>                                 <th align=\"center\"width=\"40%\"><img src=\"~/assets/images/email-temp/e-2-slider.jpg\" alt=\"\" style=\"margin-bottom: -5px;\"></td>                                 <th width=\"60%\" style=\"background-color: #11bfff;padding: 30px;\">                                     <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">                                         <tr>                                             <td style=\"color:#ffffff;font-size: 16px;line-height:20px;text-transform:uppercase;text-align:left;padding-bottom: 5px;\">New Color</td>                                         </tr>                                         <tr>                                             <td class=\"h2-white left pb20\" style=\"color:#ffffff; font-family: 'Roboto', sans-serif; font-size:52px; line-height:58px; text-transform:uppercase; font-weight:bold; text-align:left; padding-bottom:20px;\">new <br>season</td>                                         </tr>                                         <tr>                                             <td style=\"\"><p style=\"font-size:13px;color:#4e54cb;text-align:left;color:#fff;\">We are committed to your satisfaction with every order.</p></td>                                         </tr>                                                                             </table>                                     <table>                                         <tr >                                             <td class=\"text-button white-button\" style=\"font-size:14px; line-height:18px; text-align:center; text-transform:uppercase; padding:10px; background:#ffffff; color:#f54084; font-weight:bold;\"><a href=\"#\" target=\"_blan\" style=\"color:#4e54cb; text-decoration:none;\"><span style=\"color:#f1415e; text-decoration:none;\">shop now</span></a></td>                                         </tr>                                     </table>                                 </th>                             </tr>                         </table>                         <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"margin-top:30px;\">                             <thead>                                 <tr>                                     <h4 class=\"title\" style=\"width: 100%; text-align:center;margin-top: 50px;\">trending product</h4>                                     <p style=\"margin:0\">GET EVEN 25% OFF DISCOUNT</p>                                 </tr>                             </thead>                             <tr>                                 <td>                                     <div class=\"product-box hover\">                                         <div class=\"product border-theme br-0\">                                                            <img src=\"~/assets/images/email-temp/13.jpg\" alt=\"product\" style=\"width: 100%;\">                                         </div>                                         <div class=\"product-info\">                                                                        <a href=\"#\" tabindex=\"0\">                                                 <h6>When an unknown.</h6>                                             </a>                                             <h4>$45.00</h4>                                         </div>                                     </div>                                 </td>                                 <td>                                     <div class=\"product-box hover\">                                         <div class=\"product border-theme br-0\">                                             <img src=\"~/assets/images/email-temp/14.jpg\" alt=\"product\" style=\"width: 100%;\">                                         </div>                                         <div class=\"product-info\">                                             <div class=\"rating\">                                                 <i class=\"fa fa-star\"></i>                                                 <i class=\"fa fa-star\"></i>                                                 <i class=\"fa fa-star\"></i>                                                 <i class=\"fa fa-star\"></i>                                                 <i class=\"fa fa-star\"></i>                                             </div>                                             <a href=\"#\" tabindex=\"0\">                                                 <h6>When an unknown.</h6>                                             </a>                                             <h4>$45.00</h4>                                         </div>                                     </div>                                 </td>                             </tr>                         </table>                          <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" style=\"margin-top:30px;\">                             <tbody>                                 <tr align=\"center\" class=\"add-with-banner\">                                     <td>                                         <a href=\"#\"><img src=\"~/assets/images/email-temp/banner.jpg\" alt=\"product\"  style=\"width: 100%;\"></a>                                     </td>                                                                                                             </tr>                                                             </tbody>                         </table>                         <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" style=\"margin-top:30px;\">                             <tr>                                 <td>                                     <img src=\"~/assets/images/email-temp/banner-2.jpg\" alt=\"\"  style=\"width: 100%;\">                                 </td>                             </tr>                         </table>                         <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"margin-top: 30px;\">                             <tr>                                 <td align=\"center\">                                     <table align=\"center\" border=\"0\" class=\"display-width-inner\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:450px;\">                                         <tr>                                             <td align=\"center\" style=\"width: 40%;\">                                                 <img src=\"~/assets/images/email-temp/10.jpg\" alt=\"\" style=\"width: 225px;margin-bottom: -4px;\">                                             </td>                                             <td align=\"center\" style=\"background-color: #fafafa;width: 60%;\">                                                 <h3 style=\"margin: 0;\">Product One</h3>                                                 <div style=\"color:#E01931; font-weight:600; font-size:16px; line-height:27px; letter-spacing:1px;margin: 4px;\">                                                      <span style=\"color:#666666; font-weight:600; font-size:15px; line-height:25px; letter-spacing:1px;\" class=\"txt-price1\" data-color=\"Price1\" data-size=\"Price1\" data-min=\"10\" data-max=\"35\"><strike>$25.00</strike></span><span class=\"txt-price2\">&nbsp;&nbsp;&nbsp;</span>$20.90                                                 </div>                                                 <div style=\"padding: 15px 0px;text-transform: uppercase;font-size: 11px;letter-spacing: 1px;\">                                                      <a href=\"#\" style=\"color: #ffffff;text-decoration: none;background: #000;padding: 8px 12px;\">SHOP NOW</a>                                                 </div>                                             </td>                                         </tr>                                     </table>                                     <table align=\"center\" border=\"0\" class=\"display-width-inner\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:450px;\">                                         <tr>                                             <td align=\"center\" style=\"background-color: #fafafa;width: 60%;\">                                                 <h3 style=\"margin: 0;\">Product One</h3>                                                 <div style=\"color:#E01931; font-weight:600; font-size:16px; line-height:27px; letter-spacing:1px;margin: 4px;\">                                                      <span style=\"color:#666666; font-weight:600; font-size:15px; line-height:25px; letter-spacing:1px;\" class=\"txt-price1\" data-color=\"Price1\" data-size=\"Price1\" data-min=\"10\" data-max=\"35\"><strike>$25.00</strike></span><span class=\"txt-price2\">&nbsp;&nbsp;&nbsp;</span>$20.90                                                 </div>                                                 <div style=\"padding: 15px 0px;text-transform: uppercase;font-size: 11px;letter-spacing: 1px;\">                                                      <a href=\"#\" style=\"color: #ffffff;text-decoration: none;background: #000;padding: 8px 12px;\">SHOP NOW</a>                                                 </div>                                             </td>                                              <td align=\"center\" style=\"width: 40%;\">                                                 <img src=\"~/assets/images/email-temp/11.jpg\" alt=\"\" style=\"width: 225px;margin-bottom: -4px;\">                                             </td>                                         </tr>                                     </table>                                      <table align=\"center\" border=\"0\" class=\"display-width-inner\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:450px;\">                                         <tr>                                             <td align=\"center\" style=\"width: 40%;\">                                                 <img src=\"~/assets/images/email-temp/12.jpg\" alt=\"\" style=\"width: 225px;margin-bottom: -4px;\">                                             </td>                                             <td align=\"center\" style=\"background-color: #fafafa;width: 60%;\">                                                 <h3 style=\"margin: 0;\">Product One</h3>                                                 <div style=\"color:#E01931; font-weight:600; font-size:16px; line-height:27px; letter-spacing:1px;margin: 4px;\">                                                      <span style=\"color:#666666; font-weight:600; font-size:15px; line-height:25px; letter-spacing:1px;\" class=\"txt-price1\" data-color=\"Price1\" data-size=\"Price1\" data-min=\"10\" data-max=\"35\"><strike>$25.00</strike></span><span class=\"txt-price2\">&nbsp;&nbsp;&nbsp;</span>$20.90                                                 </div>                                                 <div style=\"padding: 15px 0px;text-transform: uppercase;font-size: 11px;letter-spacing: 1px;\">                                                      <a href=\"#\" style=\"color: #ffffff;text-decoration: none;background: #000;padding: 8px 12px;\">SHOP NOW</a>                                                 </div>                                             </td>                                         </tr>                                     </table>                                 </td>                             </tr>                                                     </table>                         <table class=\"main-bg-light text-center\"  align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"margin-top:30px;\">                             <tr>                                 <td style=\"padding: 30px;\">                                     <div>                                         <h4 class=\"title\" style=\"margin:0\">Follow us</h4>                                     </div>                                     <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"footer-social-icon\" align=\"center\" class=\"text-center\" style=\"margin-top:20px;\">                                         <tr>                                             <td>                                                 <a href=\"#\"><img src=\"~/assets/images/email-temp/facebook.png\" alt=\"\"></a>                                             </td>                                             <td>                                                 <a href=\"#\"><img src=\"~/assets/images/email-temp/youtube.png\" alt=\"\"></a>                                             </td>                                             <td>                                                 <a href=\"#\"><img src=\"~/assets/images/email-temp/twitter.png\" alt=\"\"></a>                                             </td>                                             <td>                                                 <a href=\"#\"><img src=\"~/assets/images/email-temp/gplus.png\" alt=\"\"></a>                                             </td>                                             <td>                                                 <a href=\"#\"><img src=\"~/assets/images/email-temp/linkedin.png\" alt=\"\"></a>                                             </td>                                             <td>                                                 <a href=\"#\"><img src=\"~/assets/images/email-temp/pinterest.png\" alt=\"\"></a>                                             </td>                                         </tr>                                                                        </table>                                     <div style=\"border-top: 1px solid #ddd; margin: 20px auto 0;\"></div>                                     <table  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"margin: 20px auto 0;\" >                                         <tr>                                             <td>                                                 <a href=\"#\" style=\"font-size:13px\">Want to change how you receive these emails?</a>                                             </td>                                         </tr>                                         <tr>                                             <td>                                                 <p style=\"font-size:13px; margin:0;\">2018 - 19 Copy Right by Themeforest powerd by Pixel Strap</p>                                             </td>                                         </tr>                                         <tr>                                             <td>                                                 <a href=\"#\" style=\"font-size:13px; margin:0;text-decoration: underline;\">Unsubscribe</a>                                             </td>                                         </tr>                                     </table>                                 </td>                             </tr>                         </table>                                             </td>                 </tr>             </tbody>                    </table>     </body> </html>";
            return htmlString;
        }
    }
}