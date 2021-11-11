using FluentValidation.AspNetCore;
using GreenPipes;
using HealthChecks.UI.Client;
using MassTransit;
using MassTransit.Pipeline.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SimpleMailService___SMS.Domain.CommandHandlers;
using SimpleMailService___SMS.Domain.Commands;
using SimpleMailService___SMS.Domain.Consumers;
using SimpleMailService___SMS.Domain.Contracts;
using SimpleMailService___SMS.Domain.Notification;
using SimpleMailService___SMS.Infra.BehaviorMediatR;
using SimpleMailService___SMS.Service;
using System;
using System.Linq;
using System.Net.Mime;

namespace SimpleMailService___SMS
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers().AddFluentValidation(config =>
				 config.RegisterValidatorsFromAssemblyContaining<SendEmailComand>());

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleMailService___SMS", Version = "v1" });
			});

			services.AddHealthChecks()
				.AddRabbitMQ(Configuration.GetConnectionString("QueueRabbitMQ"), name: "queueRabbitMQ");
			services.AddHealthChecksUI()
					.AddInMemoryStorage();


			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationRequestBehavior<,>));
			services.AddMediatR(typeof(Startup));
			services.AddScoped<IDomainNotificationContext, DomainNotificationContext>();
			services.AddScoped<ISendEmailService, SendEmailService>();
			services.AddScoped<AsyncRequestHandler<SendEmailComand>, SendEmailComandHandler>();
			services.AddScoped<AsyncRequestHandler<QueueSendEmailComand>, QueueSendEmailComandHandler>();

			Uri schedulerEndpoint = new Uri("queue:sheduleQueue");
			services.AddMassTransit(x =>
			{
				x.AddConsumer<QueueSendEmailConsumer>();

				x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
				{
					config.Host(new Uri("rabbitmq://localhost"), h =>
					{
						h.Username("guest");
						h.Password("guest");
					});


					config.ReceiveEndpoint("sheduleQueue", ep =>
					{
						ep.PrefetchCount = 10;
						ep.UseMessageRetry(r => r.Interval(3, TimeSpan.FromMinutes(1)));
						ep.ConfigureConsumer<QueueSendEmailConsumer>(provider);
					});
				}));
			});

			services.AddMassTransitHostedService();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleMailService___SMS v1"));
			}

			app.UseHealthChecks("/status",
			  new HealthCheckOptions()
			  {
				  ResponseWriter = async (context, report) =>
				  {
					  var result = JsonConvert.SerializeObject(
						  new
						  {
							  statusApplication = report.Status.ToString(),
							  healthChecks = report.Entries.Select(e => new
							  {
								  check = e.Key,
								  ErrorMessage = e.Value.Exception?.Message,
								  status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
							  })
						  });
					  context.Response.ContentType = MediaTypeNames.Application.Json;
					  await context.Response.WriteAsync(result);
				  }
			  });

			app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
			{
				Predicate = _ => true,
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});

			app.UseHealthChecksUI();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
