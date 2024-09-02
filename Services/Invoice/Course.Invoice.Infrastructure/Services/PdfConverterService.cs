using InvModel = Course.Invoice.Domain.Invoice;
using DinkToPdf.Contracts;
using System.Text;
using DinkToPdf;
using Course.Invoice.Application.Abstractions.Services;

namespace Course.Invoice.Infrastructure.Services;
public class PdfConverterService : IPdfConverterService
{
    private readonly IConverter _converter;

    public PdfConverterService(IConverter converter)
    {
        _converter = converter;
    }

    public byte[] GenerateInvoicePdf(InvModel.Invoice invoice)
    {
        var htmlContent = GenerateInvoiceHtml(invoice);

        var pdf = new HtmlToPdfDocument()
        {
            GlobalSettings = new GlobalSettings()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
            },
            Objects = {
                new ObjectSettings
                {
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                }
            }
        };

        return _converter.Convert(pdf);
    }

    private string GenerateInvoiceHtml(InvModel.Invoice invoice)
    {
        var sb = new StringBuilder();

        string head = $@"<html>
        <head>
            <style>
                $primary-color: #1779ba;
$secondary-color: #0b386f;
$gray:  #9b9b9b;
$light-gray: #eeeeee;
$medium-gray: #c8c3be;
$dark-gray: #96918c;
$black: #322d28;
$white: #f3f3f3;
$body-background: #ffffff;
$body-font-color: $black;

$sans: 'Montserrat', sans-serif;
$serif: 'Lora', Georgia, serif;



body {{
  font-family: $sans;
  font-weight: 400;
  color: $body-font-color;
}}
header.top-bar {{
  h1 {{
    font-family: $sans;
  }}
}}
main {{
  margin-top: 4rem;
  min-height: calc(100vh - 107px);
  .inner-container {{
    max-width: 800px;
    margin: 0 auto;
  }}
}}

table.invoice {{
  background: #fff;
  .num {{
    font-weight: 200;
    text-transform: uppercase;
    letter-spacing: 1.5px;
    font-size: .8em;
  }}
  tr, td {{
    background: #fff;
    text-align: left;
    font-weight: 400;
    color: $body-font-color;
  }}
  tr {{
    &.header {{
      td {{
        img {{
          max-width: 300px;
        }}
        h2 {{
          text-align: right;
          font-family: $sans;
          font-weight: 200;
          font-size: 2rem;
          color: $primary-color;
        }}
      }}
    }}
    &.intro {{
      td {{
        &:nth-child(2) {{
          text-align: right;
        }}
      }}
    }}
    &.details {{
      > td {{ 
        padding-top: 4rem; 
        padding-bottom: 0; 
      }}
      td, th {{
        &.id,
        &.qty {{
          text-align: center;
        }}
        &:last-child {{
          text-align: right;
        }}
      }}
      table {{
        thead, tbody {{
          position: relative;
          &:after {{
            content: '';
            height: 1px;
            position: absolute;
            width: 100%;
            left: 0;
            margin-top: -1px;
            background: $medium-gray;
          }}
        }}
      }}
    }}
    &.totals {{
      td {{
        padding-top: 0;
      }}
      table {{
        tr {{
          td {{
            padding-top:0;
            padding-bottom:0;
            &:nth-child(1) {{
              font-weight: 500;
            }}
            &:nth-child(2) {{
              text-align: right;
              font-weight: 200;
            }}
          }}
          &:nth-last-child(2) {{
            
            td {{
              padding-bottom: .5em;
              &:last-child {{
                position: relative;
                &:after {{
                  content: '';
                  height: 4px;
                  width: 110%;
                  border-top: 1px solid $primary-color;
                  border-bottom: 1px solid $primary-color;
                  position: relative;
                  right: 0;
                  bottom: -.575rem;
                  display: block;
                }}
              }}
            }}
            
          }}
          &.total {{
            td {{
              font-size: 1.2em;
              padding-top: .5em;
              font-weight: 700;
              &:last-child {{
                font-weight: 700;
              }}
            }}
          }}
        }}
      }}
    }}
  }}
}}

.additional-info {{
  h5 {{
    font-size: .8em;
    font-weight: 700;
    text-transform: uppercase;
    letter-spacing: 2px;
    color: $primary-color;
  }}
}}
            </style>
        </head>";

        sb.Append(head);

        string bodyFirst = $@"<body>
            <div class=""row expanded"">
  <main class=""columns"">
    <div class=""inner-container"">
    <section class=""row"">
      <div class=""callout large invoice-container"">
        <table class=""invoice"">
          <tr class=""header"">
            <td class="""">
              <h2>Course Microservice</h2>
                </td>
            <td class=""align-right"">
              <h2>Invoice</h2>
            </td>
          </tr>
          <tr class=""intro"">
            <td class="""">
              {invoice.Customer.UserName}<br>
              Thank you for your order.
            </td>
            <td class=""text-right"">
              <span class=""num"">Order #{invoice.Id}</span><br>
              {invoice.CreatedDate:MM/dd/yy H:mm:ss zzz}
            </td>
          </tr>
          <tr class=""details"">
            <td colspan=""2"">
              <table>
                <thead>
                  <tr>
                    <th class=""desc"">Item Description</th>
                    <th class=""id"">Item ID</th>
                    <th class=""qty"">Quantity</th>
                    <th class=""amt"">Subtotal</th>
                  </tr>
                </thead>
                <tbody>";

        sb.Append(bodyFirst);

        foreach (var orderItem in invoice.OrderInformation.OrderItems)
        {
            sb.Append($@"  
                  <tr class=""item"">
                    <td class=""desc"">{orderItem.ProductName}</td>
                    <td class=""id num"">{orderItem.Id}</td>
                    <td class=""qty"">1</td>
                    <td class=""amt"">${orderItem.Price}</td>
                  </tr>");
        }

        string secondBody = $@"</tbody>
              </table>
            </td> 
          </tr>
          <tr class=""totals"">
            <td></td>
            <td>
              <table>
                <tr class=""subtotal"">
                  <td class=""num"">Subtotal</td>
                  <td class=""num"">${invoice.OrderInformation.GetTotalPrice}</td>
                </tr>
                <tr class=""fees"">
                  <td class=""num"">Shipping & Handling</td>
                  <td class=""num"">$0.00</td>
                </tr>
                <tr class=""total"">
                  <td>Total</td>
                  <td>${invoice.OrderInformation.GetTotalPrice}</td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
        
        <section class=""additional-info"">
        <div class=""row"">
          <div class=""columns"">
            <h5>Billing Information</h5>
            <p>{invoice.Customer.Address.District}<br>
              {invoice.Customer.Address.Street}<br>
              {invoice.Customer.Address.Line}<br>
              {invoice.Customer.Address.Province} {invoice.Customer.Address.ZipCode}<br>
              </p>
          </div>
        </div>
        </section>
      </div>
    </section>
    </div>
  </main>
</div>
        </body>
        </html>";

        sb.Append(secondBody);

        return sb.ToString();
    }
}
