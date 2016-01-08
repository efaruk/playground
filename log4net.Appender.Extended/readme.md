Time and Count Buffered BufferingAppenderSkeleton: DoubleBufferingAppenderSkeleton
===================
[![efaruk MyGet Build Status](https://www.myget.org/BuildSource/Badge/efaruk?identifier=80412953-5b8c-4baf-bc52-a5331755bdef)](https://www.myget.org/)

[![NuGet version](https://badge.fury.io/nu/log4net.Appender.Extended.svg)](https://badge.fury.io/nu/log4net.Appender.Extended)

Example Config:

	<appender name="CustomDoubleBufferingAppender" type="log4net.Appender.Extended.Custom.CustomDoubleBufferingAppender">
      <MaxBufferSize value="10"/>
      <TimeThreshold value="5"/>
      <EnvironmentVariablesLevel value="FATAL"/>
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%message : %exception"/>
      </layout>
      <parameter>
        <parameterName value="StackTrace"/>
        <layout type="log4net.Layout.PatternLayout">
          <conversionPattern value="%stacktrace{5}"/>
        </layout>
      </parameter>
      <parameter>
        <parameterName value="Exception"/>
        <levelMin value="ERROR"/>
        <layout type="log4net.Layout.PatternLayout">
          <conversionPattern value="%exception"/>
        </layout>
      </parameter>
      <parameter>
        <parameterName value="AspNetCache"/>
        <levelMin value="ERROR"/>
        <layout type="log4net.Appender.Extended.Layout.ExtendedPatternLayout">
          <conversionPattern value="%extended-aspnet-cache{*}"/>
        </layout>
      </parameter>
      <parameter>
        <parameterName value="AspNetContext"/>
        <levelMin value="ERROR"/>
        <layout type="log4net.Appender.Extended.Layout.ExtendedPatternLayout">
          <conversionPattern value="%extended-aspnet-context{*}"/>
        </layout>
      </parameter>
      <parameter>
        <parameterName value="CustomItem"/>
        <omitNull value="true" />
        <levelMin value="ALL"/>
        <layout type="log4net.Appender.Extended.Layout.ExtendedPatternLayout">
          <conversionPattern value="%extended-aspnet-context{custom_item}"/>
        </layout>
      </parameter>
      <parameter>
        <parameterName value="AspNetRequest"/>
        <levelMin value="ERROR"/>
        <layout type="log4net.Appender.Extended.Layout.ExtendedPatternLayout">
          <conversionPattern value="%extended-aspnet-request{*}"/>
        </layout>
      </parameter>
      <parameter>
        <parameterName value="AspNetSession"/>
        <levelMin value="ERROR"/>
        <layout type="log4net.Appender.Extended.Layout.ExtendedPatternLayout">
          <conversionPattern value="%extended-aspnet-session{*}"/>
        </layout>
      </parameter>
      <filter type="log4net.Filter.LevelRangeFilter">
        <levelMin value="ALL"/>
        <levelMax value="OFF"/>
      </filter>
    </appender>


Example Code to Extend:

	public class CustomDoubleBufferingAppender: DoubleBufferingAppenderSkeleton
    {
        protected override void BulkSend(IEnumerable<ExtendedLoggingEvent> customLoggingEvents)
        {
            //send bulk events to some where...
        }
    }

If you need custom parameters other then extend, override ConvertLoggingEvent:

	
	protected override ExtendedLoggingEvent ConvertLoggingEvent(LoggingEvent loggingEvent, IEnumerable<LayoutParameter> parameters)
    {
        var extendedLoggingEvent = base.ConvertLoggingEvent(loggingEvent, parameters);
        extendedLoggingEvent.CustomParameters.Add(new ExtendedLoggingEventParameter("CustomParameter1", "CustomValue"));
        return extendedLoggingEvent;
    }

Happy logging...

Instead of aspnet-cache, aspnet-context, aspnet-request, aspnet-session you can use wild card option with; extended-aspnet-cache{*}, extended-aspnet-context{*}, extended-aspnet-request{*}, extended-aspnet-session{*}
If you want to set custom parameter value programmatically you can set HttpContext.Items["custom_item"] and use in log parameters : %extended-aspnet-context{custom_item}.

